CREATE OR ALTER PROCEDURE sp_finish_video_upload
    @p_inserted_video_id	BIGINT OUTPUT,
	@p_channel_id			BIGINT,
    @p_title				NVARCHAR(255),
    @p_description			NVARCHAR(1000),
    @p_visibility			NVARCHAR(255),
    @p_is_monetized			BIT,
    @p_duration				BIGINT,
    @p_thumbnail_media_id	BIGINT,
    @p_video_media_id		BIGINT,
    @p_playlist_ids			dbo.z_bigint_list READONLY,  -- user-defined table type
    @p_category_ids			dbo.z_bigint_list READONLY,  -- user-defined table type
    @p_chapters				dbo.z_chapter_list READONLY  -- user-defined table type
AS
BEGIN
	SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Ensure channel exists
        IF NOT EXISTS (SELECT 1 FROM z_channel WHERE channel_id = @p_channel_id)
            THROW 50000, 'Channel does not exist.', 1;

        -- Validate thumbnail media
        IF NOT EXISTS (
            SELECT 1 FROM z_media m
            WHERE m.media_id = @p_thumbnail_media_id AND m.[type] = 'image'
        )
            THROW 50001, 'Thumbnail media is invalid.', 1;

        -- Validate video media
        IF NOT EXISTS (
            SELECT 1 FROM z_media
            WHERE media_id = @p_video_media_id AND type = 'video'
        )
            THROW 50003, 'Video media is invalid.', 1;

		-- Validate categories
		IF EXISTS (
			SELECT 1 
			FROM @p_category_ids pc
			WHERE NOT EXISTS (SELECT 1 FROM z_category c WHERE pc.[value] = c.category_id)
		)
			THROW 50002, 'Category doesnt exist.', 1;

		-- Validate chapters are within the videos duration
		IF EXISTS (
			SELECT 1 FROM @p_chapters pc
			WHERE NOT (pc.start_time >= 0 AND pc.start_time <= @p_duration)
		)
			THROW 50002, 'Chapter start_time out of range.', 1;


        -- Insert video
        INSERT INTO z_video (
            channel_id, thumbnail_id, video_file_id,
            visibility, is_monetized, title, description,
            upload_date, duration, view_count, like_count,
            dislike_count, comment_count
        )
        VALUES (
            @p_channel_id, @p_thumbnail_media_id, @p_video_media_id,
            @p_visibility, @p_is_monetized, @p_title, @p_description,
            GETUTCDATE(), @p_duration, 0, 0, 0, 0
        );

        DECLARE @video_id BIGINT = SCOPE_IDENTITY();

        -- Insert into z_video_category
        INSERT INTO z_video_category (video_id, category_id)
        SELECT @video_id, pc.[value] FROM @p_category_ids pc;

        -- Insert into z_playlist_video with ordering
        DECLARE @playlist_id BIGINT, 
			    @next_order INT;
		
		DECLARE playlist_cursor CURSOR FOR SELECT p.[value] FROM @p_playlist_ids p;
			OPEN playlist_cursor;
				FETCH NEXT FROM playlist_cursor INTO @playlist_id;

				WHILE @@FETCH_STATUS = 0 
				BEGIN
					SELECT @next_order = ISNULL(MAX(pv.[order]), 0) + 1
					FROM z_playlist_video pv
					WHERE playlist_id = @playlist_id;

					INSERT INTO z_playlist_video (playlist_id, video_id, [order])
					VALUES (@playlist_id, @video_id, @next_order);

					FETCH NEXT FROM playlist_cursor INTO @playlist_id;
				END
			CLOSE playlist_cursor;
		DEALLOCATE playlist_cursor;

        -- Insert video chapters
		INSERT INTO z_video_chapter (video_id, title, start_time)
		SELECT @video_id, pc.title, pc.start_time
		FROM @p_chapters pc;

		COMMIT;
		SET @p_inserted_video_id = @video_id;
	END TRY
	BEGIN CATCH
		ROLLBACK;
		THROW;
	END CATCH
END