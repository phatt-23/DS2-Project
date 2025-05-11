///////////////////////////////////
/// Functions /////////////////////
///////////////////////////////////

// 9 DB functions (touching the database) + 9 APP functions (purely in application)

// DB
func uploadVideoFile            (filepath string)               Media
// DB
func uploadThumbnailFile        (filepath string)               Media
// DB
func getUsersByName             (searchUsername string)         []User
// APP
func getUserById                (userId int)                    User                   
// DB
func getChannelsFromUser        (userId int)                    []Channel
// APP
func getChannelById             (channelId int)                 Channel          
// APP
func addVideoChapter            (videoId int, title string, 
                                 startTime int, endTime int?)   void
// DB 
func getChannelPlaylists        (channelId int)                 []Playlist
// DB
func insertChannelPlaylist      (userId int, channelId int, 
                                 title string, 
                                 visibility VisibilityEnum)     Playlist 
// APP
func addVideoToPlaylist         (playlistId int)                void
// APP
func removeVideoFromPlaylist    (playlistId int)                void
// DB
func getVideoCategoriesByName   (categoryName string)           []Category
// APP
func addVideoCategory           (category string)               void
// APP
func removeVideoCategory        (category string)               void
// DB
func getAdvertisementsByTitle   (searchAdTitle string)          []Advertisement
// APP
func addVideoAdvertisement      (advertisementId int, 
                                 startTime int)                 void
// APP
func removeVideoAdvertisement   (videoAdvertisementId int)      void
// DB
func finishVideoUpload(                     
    channelId           int,                // Channel ID where the video is uploaded
    title               string,             // Title of the video
    description         string,             // Video description (optional)
    visibility          VisibilityEnum,     // (public, private, unlisted, draft)
    isMonetized         bool,               // Monetization flag (1 or 0)
    thumbnailMediaId    int,                // Media ID of uploaded thumbnail
    videoMediaId        int,                // Media ID of uploaded video file
    duration            int,                // Duration of video (seconds)
    playlistIds         []int,              // IDs of playlists to add video
    categoryIds         []int,              // IDs of categories
    adPlacements        []AdPlacement,      // Ads placed in the video (if monetized)
    chapters            []Chapter,          // Video chapters
) Video                                     // Newly inserted video 

////////////////////////////////////////////////////////////////////
// 1. upload thumbnail and video files /////////////////////////////
////////////////////////////////////////////////////////////////////


// 1.1 DB -> p_videoMediaId
func uploadVideoFile(filepath string) Media {            
    var url string := createUrl(filepath) 
    var blob Blob := getBlobData(filepath) 

    var media Media := INSERT INTO media(url, type, data)
                       VALUES (url, 'video', blob) 
    return media 
}


// 1.2 DB -> p_thumbnailMediaId
func uploadThumbnailFile(filepath string) Media {        
    var url string := createUrl(filepath) 
    var blob Blob := getBlobData(filepath) 

    var media Media := INSERT INTO media(url, type, data)
                       VALUES (url, 'thumbnail', blob) 
    return media 
}




////////////////////////////////////////////////////////////////////
// 2. identify channel for which to upload the video ///////////////
////////////////////////////////////////////////////////////////////


// 2.1 DB (active and not-deleted)
func getUsersByName(searchUsername string) []User {     
    var users []User := SELECT * 
                          FROM user 
                         WHERE username LIKE '%searchUsername%'
                           AND status == 'active' 
                           AND is_deleted == 0 
    return users 
}


// 2.2 APP
func getUserById(userId int) User                   


// 2.3 DB   
func getChannelsFromUser(userId int) []Channel {    
    var channels []Channel := SELECT *
                                FROM channel
                               WHERE user_id == userId 
                                 AND is_deleted == 0 
    return channels 
}


// 2.4 APP -> p_channelId
func getChannelById(channelId int) Channel          



////////////////////////////////////////////////////////////////////
// 3. add chapters to the video ////////////////////////////////////
////////////////////////////////////////////////////////////////////


// 3.1 APP -> +p_chapters
func addVideoChapter(videoId int, title string, startTime int, endTime int?)   



////////////////////////////////////////////////////////////////////
// 4. add this video to channel playlists //////////////////////////
////////////////////////////////////////////////////////////////////


// 4.1 DB (not-deleted)
func getChannelPlaylists(channelId int) []Playlist {    
    var playlists []Playlist := SELECT * 
                                  FROM playlist 
                                 WHERE channel_id = channelId 
                                   AND is_deleted = 0 
    return playlists 
}


// 4.2 DB  
func insertChannelPlaylist(
    userId int, channelId int, title string, visibility VisibilityEnum
) Playlist {
    var playlist Playlist := INSERT INTO playlist(
                                user_id, channel_id, title, visibility
                             ) VALUES (userId, channelId, title, visibility) 
    return playlist 
}


// 4.3 APP -> +p_playlistsIds
func addVideoToPlaylist(playlistId int)                 


// 4.4 APP -> -p_playlistsIds
func removeVideoFromPlaylist(playlistId int)            


////////////////////////////////////////////////////////////////////
// 5. assing the video to categories ///////////////////////////////
////////////////////////////////////////////////////////////////////


// 5.1 DB
func getVideoCategoriesByName(categoryName string) []Category {     
    var categories []Category := SELECT * 
                                   FROM category 
                                  WHERE category_name LIKE '%categoryName%' 
    return categories 
}


// 5.2 APP -> +p_categories
func addVideoCategory(category string)                           

// 5.3 APP -> -p_categories
func removeVideoCategory(category string)                        

////////////////////////////////////////////////////////////////////
// 6. add advertisements to the video //////////////////////////////
////////////////////////////////////////////////////////////////////


// 6.1 DB (where status == 'active')
func getAdvertisementsByTitle(searchAdTitle string) []Advertisement {     
    var ads []Advertisement := SELECT * 
                                 FROM advertisement 
                                WHERE title LIKE '%searchAdTitle%'
                                  AND status = 'active' 
    return ads:
}


// 6.2 APP -> +p_adPlacements
func addVideoAdvertisement(advertisementId int, startTime int)      


// 6.3 APP -> -p_adPlacements
func removeVideoAdvertisement(videoAdvertisementId int)




////////////////////////////////////////////////////////////////////
// 7. finalize by calling a transaction ////////////////////////////
////////////////////////////////////////////////////////////////////


// DB Transaction 
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



.
