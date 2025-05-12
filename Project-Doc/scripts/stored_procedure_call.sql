BEGIN
	-- Declare variables for output and table-valued parameters
	DECLARE @category_ids		dbo.z_bigint_list,
			@playlist_ids		dbo.z_bigint_list,
			@chapters			dbo.z_chapter_list,
			@inserted_video_id	BIGINT;

	-- Add sample category IDs
	INSERT INTO @category_ids (value) 
	VALUES (1), (2), (3);

	-- Add sample playlist IDs
	INSERT INTO @playlist_ids (value) 
	VALUES (10), (11);

	-- Add sample chapters
	INSERT INTO @chapters (title, start_time)
	VALUES 
		(N'Intro', 0),
		(N'Main Part', 61);

	-- Call the procedure
	EXEC sp_finish_video_upload
		@p_channel_id			= 12,
		@p_title				= N'Test Video',
		@p_description			= N'Inserted through SQL procedure call.',
		@p_visibility			= N'public',
		@p_is_monetized			= 1,
		@p_duration				= 360,
		@p_thumbnail_media_id	= 35,
		@p_video_media_id		= 11,
		@p_category_ids			= @category_ids,
		@p_playlist_ids			= @playlist_ids,
		@p_chapters				= @chapters,
		@p_inserted_video_id	= @inserted_video_id OUTPUT;

	-- Check the result
	SELECT @inserted_video_id AS InsertedVideoId;
END


SELECT *
from
