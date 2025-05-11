//////////////////////////////////////////////////
// TRANSACTION MINISPECIFICATION /////////////////
//////////////////////////////////////////////////

type AdPlacement struct {
    adId       int
    startTime  int
}

type Chapter struct {
    title       string
    startTime   int
    endTime?    int
}

type VisibilityEnum enum {
    PUBLIC 
    PRIVATE 
    UNLISTED 
    DRAFT
}

func finalizeVideoUpload(
    p_channelId             int,                // Channel ID where video is uploaded
    p_title                 string,             // Title of the video
    p_description           string,             // Video description (optional)
    p_visibility            VisibilityEnum,     // (public, private, unlisted, draft)
    p_isMonetized           bool,               // Monetization flag (1 or 0)
    p_thumbnailMediaId      int,                // Media ID of uploaded thumbnail
    p_videoMediaId          int,                // Media ID of uploaded video file
    p_duration              int,                // Duration of video (seconds)
    p_playlistIds           []int,              // IDs of playlists to add video
    p_categoryIds           []int,              // IDs of categories
    p_adPlacements          []AdPlacement,      // Ads placed in the video 
    p_chapters              []Chapter,          // Video chapters
) Video {                                       // Newly inserted video 
    beginTransaction() 

    // CHANNEL VALIDATION: Verify that the provided channel exists.
    if empty(SELECT * FROM channel WHERE channel_id = p_channelId) {
        rollback() 
        raise Error("Channel with id {p_channelId} doesnt exist") 
    }
    

    // ADS VALIDATION: All ads should exist and be active.
    for advertisement in p_advertisements {
        var ads []Advertisement = SELECT * FROM advertisement 
                                   WHERE advertisement_id = advertisement.adId 
        if empty(ads) {
            rollback() 
            raise Error("Ad with id of {advertisement.adId} doesn't exist.") 
        }

        var ad = single(ads) 
        if ad.status != AdvertisementStatus.ACTIVE {
            rollback() 
            raise Error("Ad with if of {advertisement.adId} isn't active.") 
        }
    }
    // MEDIA VALIDATION: 
    // Verify that the thumbnail and video file exist and are of correct media type.
    // (for video media.type = 'video' and for thumbnail media.type = 'thumbnail')

    // If the query result is empty then no thumbnail with provided id exists.
    if empty(SELECT * FROM media WHERE media_id = p_thumbnailMediaId) {
        rollback() 
        raise Error("Thumbnail media doesn't exist.") 
    }

    // Get the thumbnail media by id 
    // and check that thumbnail media has its type set to 'thumbnail'.
    var thumbnailMedia media := single(SELECT * FROM media 
                                        WHERE media_id = p_thumbnailMediaId) 
    if thumbnailMedia.type != 'thumbnail' {
        rollback() 
        raise Error("For thumbnail media expected type 'thumbnail'.")
    }
    

    // If the query result is empty then no video media with provided id exists.
    if empty(SELECT * FROM media WHERE media_id = p_videoMediaId) {
        rollback()
        raise Error("Video media doesn't exist.") 
    }
    
    // Get the video media by id and check that its type is set to 'video'.
    var videoMedia media := single(SELECT * FROM media WHERE media_id = p_videoMediaId) 
    if videoMedia.type != 'video' {
        rollback()
        raise Error("For video media expected type 'video'.")
    }
    

    // PLAYLIST VALIDATION: 
    // Validate that each playlist is owned by the selected channel.
    var channelPlaylists []int = SELECT playlist_id FROM playlist 
                                  WHERE channel_id = p_channelId
    for playlistId in p_playlistIds {
        if !channelPlaylists.contains(playlistId) {
            rollback() 
            raise Error("Playlist {playlistId} doesn't belong \
                         to channel with id {p_channelId}") 
        }
    }


    // CHAPTER VALIDATION:
    // Ensure chapters are in sequential order and non-overlapping.
    for chapter in chapters.sort(start_time, Order.ASCENDING) {       

        // If the end_time is defined, then it must come after start_time.
        if chapter.end_time != null && chapter.end_time <= chapter.start_time {
            rollback() 
            raise Error("Chapter end time must be greater than start time") 
        }
        
        // The current chater's end_time must be 
        // less then the start_time of the next chapter.
        if currentChapter.end_time > nextChapter.start_time {
            rollback() 
            raise Error("Chapters cannot overlap") 
        }
    }
    // Insert the video record 
    var newVideo video := INSERT INTO video (
                              channel_id,  thumbnail_id,  video_file_id,
                              visibility,  is_monetized,  is_deleted,
                              title,       description,   upload_date,    duration,
                              view_count,  like_count,    dislike_count,  comment_count
                          ) VALUES (
                              p_channelId,  p_thumbnailMediaId, p_videoMediaId, 
                              p_visibility, p_isMonetized,      false, 
                              p_title,      p_description,      GETDATE(), p_duration,
                              0,            0,                  0,         0
                          ) 


    // Insert each selected category into video_category table.
    for categoryId in p_category_ids {
        INSERT INTO video_category (video_id, category_id)
        VALUES (newVideo.video_id, categoryId) 
    }


    // Insert each selected playlist into playlist_video table.
    for playlistId in p_playlist_ids {
        // Determine the maximum order currently in the playlist.
        var nextOrder int := SELECT COALESCE(MAX(order), 0) + 1 
                              FROM playlist_video 
                             WHERE playlist_id = playlistId 
        
        INSERT INTO playlist_video(playlist_id, video_id, added_date, order)
        VALUES (p_playlistId, newVideo.video_id, GETDATE(), nextOrder) 
    }


    // Ads only allowed if video is monetized, skip if monetization is disabled.
    // If video is monetized, insert advertisements into video_advertisement join table.
    if p_isMonetized == true {
        for advertisement in p_adPlacements {
            INSERT INTO video_advertisement (video_id, advertisement_id, start_time)
            VALUES (newVideo.video_id, advertisement.adId, advertisement.startTime) 
        }
    }


    // Insert chapters into video_chapter table.
    for chapter in p_chapters {
        INSERT INTO video_chapter (video_id, title, start_time, end_time)
        VALUES (
            newVideo.video_id, p_chapterTitle, 
            chapter.startTime, chapter.endTime
        ) 
    }


    // End Transaction.
    endTransaction()  


    // Return id of the newly inserted video.
    return newVideo.video_id 
}


.
