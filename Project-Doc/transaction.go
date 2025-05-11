///////////////////////////////////
/// Functions /////////////////////
///////////////////////////////////

// 7 DB functions
// 9 APP functions

////////////////////////////////////////////////////////////////////
// 1. upload thumbnail and video files /////////////////////////////
////////////////////////////////////////////////////////////////////

// DB -> p_videoMediaId
// 1.1
func uploadVideoFile(filepath string) Media {            
    var url string := createUrl(filepath);
    var blob Blob := getBlobData(filepath);

    var media Media := INSERT INTO media(url, type, data)
                       VALUES (url, 'video', blob);
    return media;
}

// DB -> p_thumbnailMediaId
// 1.2
func uploadThumbnailFile(filepath string) Media {        
    var url string := createUrl(filepath);
    var blob Blob := getBlobData(filepath);

    var media Media := INSERT INTO media(url, type, data)
                       VALUES (url, 'thumbnail', blob);
    return media;
}

////////////////////////////////////////////////////////////////////
// 2. identify channel for which to upload the video ///////////////
////////////////////////////////////////////////////////////////////

// DB (active and not-deleted)
// 2.1
func getUsersByName(searchUsername string) []User {     
    var users []User := SELECT * 
                        FROM user 
                        WHERE username LIKE '%searchUsername%'
                          AND status == 'active' 
                          AND is_deleted == 0;
    return users;
}

// APP
// 2.2
func getUserById(userId int) User                   

// DB   
// 2.3
func getChannelsFromUser(userId int) []Channel {    
    var channels []Channel := SELECT *
                              FROM channel
                              WHERE user_id == userId 
                              AND is_deleted == 0;
    return channels;
}

// APP -> p_channelId
// 2.4
func getChannelById(channelId int) Channel          

////////////////////////////////////////////////////////////////////
// 3. add chapters to the video ////////////////////////////////////
////////////////////////////////////////////////////////////////////

// APP -> +p_chapters
// 3.1
func addVideoChapter(videoId int, title string, 
                     startTime int, endTime int?)   

////////////////////////////////////////////////////////////////////
// 4. add this video to channel playlists //////////////////////////
////////////////////////////////////////////////////////////////////

// DB (not-deleted)
// 4.1
func getChannelPlaylists(channelId int) []Playlist {    
    var playlists []Playlist := SELECT * 
                                FROM playlist 
                                WHERE channel_id = channelId 
                                  AND is_deleted = 0;
    return playlists;
}

// DB  
// 4.2
func insertChannelPlaylist(userId int, channelId int, title string, visibility VisibilityEnum) Playlist {
    var playlist Playlist := INSERT INTO playlist(user_id, channel_id, title, visibility)
                             VALUES (userId, channelId, title, visibility);
    return playlist;
}

// APP -> +p_playlistsIds
// 4.3
func addVideoToPlaylist(playlistId int)                 

// APP -> -p_playlistsIds
// 4.4
func removeVideoFromPlaylist(playlistId int)            

////////////////////////////////////////////////////////////////////
// 5. assing the video to categories ///////////////////////////////
////////////////////////////////////////////////////////////////////

// DB
// 5.1
func getVideoCategoriesByName(categoryName string) []Category {     
    var categories []Category := SELECT * 
                                 FROM category 
                                 WHERE category_name LIKE '%categoryName%';
    return categories;
}

// APP -> +p_categories
// 5.2
func addVideoCategory(category string)                           

// APP -> -p_categories
// 5.3
func removeVideoCategory(category string)                        

////////////////////////////////////////////////////////////////////
// 6. add advertisements to the video //////////////////////////////
////////////////////////////////////////////////////////////////////

// DB (where status == 'active')
// 6.1
func getAdvertisementsByTitle(searchAdName string) []Advertisement {     
    var ads []Advertisement := SELECT * 
                               FROM advertisement 
                               WHERE title LIKE '%searchAdName%'
                                 AND status = 'active';
    return ads:
}

// APP -> +p_adPlacements
// 6.2
func addVideoAdvertisement(advertisementId int, startTime int)      

// APP -> -p_adPlacements
// 6.3
func removeVideoAdvertisement(videoAdvertisementId int)

////////////////////////////////////////////////////////////////////
// 7. finalize by calling a transaction ////////////////////////////
////////////////////////////////////////////////////////////////////

// APP or DB function
func finishVideoUpload(                     
    channelId           int,                // Channel ID where the video is uploaded
    title               string,             // Title of the video
    description         string,             // Video description (optional)
    visibility          VisibilityEnum,     // (public, private, unlisted, draft)
    isMonetized         boolean,            // Monetization flag (1 or 0)
    thumbnailMediaId    int,                // Media ID of uploaded thumbnail
    videoMediaId        int,                // Media ID of uploaded video file
    duration            int,                // Duration of video (seconds)
    playlistIds         []int,              // IDs of playlists to add video
    categoryIds         []int,              // IDs of categories
    adPlacements        []AdPlacement,      // Ads placed in the video (if monetized)
    chapters            []Chapter,          // Video chapters
) Video                                     // Newly inserted video 


//////////////////////////////////////////////////
// TRANSACTION MINISPECIFICATION /////////////////
//////////////////////////////////////////////////

type AdPlacement struct {
    adId       bigint
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
    p_channelId             int,                // Channel ID where the video is uploaded
    p_title                 string,             // Title of the video
    p_description           string,             // Video description (optional)
    p_visibility            VisibilityEnum,     // (public, private, unlisted, draft)
    p_isMonetized           boolean,            // Monetization flag (1 or 0)
    p_thumbnailMediaId      int,                // Media ID of uploaded thumbnail
    p_videoMediaId          int,                // Media ID of uploaded video file
    p_duration              int,                // Duration of video (seconds)
    p_playlistIds           []int,              // IDs of playlists to add video
    p_categoryIds           []int,              // IDs of categories
    p_adPlacements          []AdPlacement,      // Ads placed in the video (if monetized)
    p_chapters              []Chapter,          // Video chapters
) Video {                                       // Newly inserted video 
    beginTransaction();

    // CHANNEL VALIDATION:
    // Verify that the provided channel exists.
    if !any(SELECT * FROM channel WHERE channel_id = p_channelId) {
        raise Error("Channel with id {p_channelId} doesnt exist");
    }
    
    // ADS VALIDATION: 
    // All ads should exist and be active.
    for advertisement in p_advertisements {
        var ads []Advertisement = SELECT * FROM advertisement WHERE advertisement_id = advertisement.adId
        if !any(ads) {
            raise Error("Advertisement with id of {advertisement.adId} doesn't exist.");
        }

        var ad = single(ads);
        if ad.status != AdvertisementStatus.ACTIVE {
            raise Error("Advertisement with if of {advertisement.adId} isn't active.")
        }
    }

    // MEDIA VALIDATION: 
    // Verify thumbnail (media.type = 'thumbnail') and video file (media.type = 'video').

    // if the query result is empty then raise an error
    if !any(SELECT * FROM media WHERE media_id = p_thumbnailMediaId) {
        raise Error("Thumbnail media doesn't exist.");
    }

    // get the thumbnail media by id
    var thumbnailMedia media := single(SELECT * FROM media WHERE media_id = p_thumbnailMediaId)

    // thumbnail media must have its type set as 'thumbnail'
    if thumbnailMedia.type != 'thumbnail' {
        raise Error("For thumbnail media expected type 'thumbnail'. Instead got {thumbnail_media.type}");
    }
       
    // if the query result is empty then raise an error
    if !any(SELECT * FROM media WHERE media_id = p_videoMediaId) {
        raise Error("Video media doesn't exist.");
    }
    
    // get the video media by id
    var videoMedia media := single(SELECT * FROM media WHERE media_id = p_videoMediaId)
    
    // video media must have its type set as 'video'
    if videoMedia.type != 'video' {
        raise Error("For video media expected type 'video'. Instead got {video_media.type}");
    }
    

    // PLAYLIST VALIDATION: 
    // Validate each playlist is owned by the selected channel or its user.
    var channelPlaylists []int = [SELECT playlist_id FROM playlist WHERE channel_id = p_channelId];

    for playlist_id in p_playlist_ids {
        if !channelPlaylists.contains(playlist_id) {
            raise Error("Playlist {playlist_id} doesn't belong to channel with id {p_channelId}");
        }
    }


    // CHAPTER VALIDATION:
    // Ensure chapters are in sequential order and non-overlapping:
    for chapter in chapters.sort(start_time, Order.ASCENDING) {       
        if chapter.end_time != null && chapter.end_time <= chapter.start_time {
            raise Error("Chapter end time must be greater than start time");
        }

        if current_chapter.end_time > next_chapter.start_time {
            raise Error("Chapters cannot overlap");
        }
    }


    // Insert the video record
    var newVideo video := INSERT INTO video (
                              channel_id, thumbnail_id, video_file_id, visibility, is_monetized, 
                              is_deleted, title, description, upload_date, duration,
                              view_count, like_count, dislike_count, comment_count
                          ) VALUES (
                              p_channelId, p_thumbnailMediaId, p_videoMediaId, p_visibility, p_isMonetized,
                              0, p_title, p_description, GETDATE(), p_duration,
                              0, 0, 0, 0
                          );

    // Insert into video_category table for each selected category
    for category_id in p_category_ids {
        INSERT INTO video_category (video_id, category_id)
        VALUES (newVideo.video_id, p_category_id);
    }

    // Insert into playlist_video for each selected playlist
    for playlist_id in p_playlist_ids {
        // Determine the maximum order currently in the playlist
        var nextOrder int := SELECT MAX(COALESCE(MAX(order), 0)) + 1 
                                 FROM playlist_video 
                                 WHERE playlist_id = p_playlistId;

        INSERT INTO playlist_video(playlist_id, video_id, added_date, order)
        VALUES (p_playlistId, newVideo.video_id, GETDATE(), nextOrder);
    }

    // Advertisement Validation:
    // Ads only allowed if video is monetized; skip if monetization is disabled.
    // If video is monetized (is_monetized = 1), insert advertisements
    if p_is_monetized == true {
        for advertisement in p_adPlacements {
            INSERT INTO video_advertisement (video_id, advertisement_id, start_time)
            VALUES (newVideo.video_id, advertisement.adId, advertisement.startTime);
        }
    }

    // Insert chapters into video_chapter table
    for chapter in p_chapters {
        INSERT INTO video_chapter (video_id, title, start_time, end_time)
        VALUES (newVideo.video_id, p_chapter_title, chapter.startTime, chapter.endTime);
    }

    // End Transaction
    endTransaction(); 
    
    // Return id of the newly inserted video
    return newVideo.video_id;
}

