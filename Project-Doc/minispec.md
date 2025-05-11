# Form Description:

- Upload of a new video.

1. Select a user for which to add the video.
2. Choose one of the his channels to which to add the video.
3. Input details - title, description, visibility, monetization
    (setting monetization to false will disable addition of ads to the video)
4. Upload a thumbnail and the video file.
5. Choose playlists to which to add the video.
6. Choose categories to which to add the video.
7. Search advertisements by title or (and) content. Choose one and specify where to put it into the video.
8. Segment the video into video chapters.
9. Submit the video upload.


# Functions:



# Minispecification of Transaction: "Finalize Video Upload"

The transaction finalizes the upload of a new video, linking all metadata (categories, chapters, advertisements, playlists, thumbnail, and media) to the selected user and their chosen channel.


## Input Parameters
| Parameter | Type | Description |
|---|---|---|
| channel_id | bigint | Channel ID where the video is uploaded |
| title | nvarchar(255) | Title of the video| 
| description | nvarchar(1000) | Video description (optional)| 
| visibility | enum | Visibility (public, private, unlisted, draft)| 
| is_monetized | bit | Monetization flag (1 or 0)| 
| thumbnail_media_id | bigint | Media ID of uploaded thumbnail| 
| video_media_id | bigint | Media ID of uploaded video file| 
| duration | bigint | Duration of video (seconds)| 
| playlist_ids | List<bigint> | IDs of playlists to add video| 
| category_ids | List<bigint> | IDs of categories| 
| advertisements | List<{ad_id: bigint, start_time: integer, end_time: integer}> | Ads placed in the video (if monetized)| 
| chapters | List<{title: nvarchar(100), start_time: integer, end_time: integer}> | Video chapters|  

## Transaction Output:
| Output | Type | Description |
|---|---|---|
| video_id |  bigint | Newly created video ID |

## Preconditions:
1) Provided channel_id must exist and not be deleted (channel.is_deleted = 0).
2) Provided thumbnail_media_id and video_media_id must exist and have correct media.type.
3) All provided category_ids must exist.
4) All provided playlist_ids must exist and belong to selected channel or user.
5) Advertisements are optional; if is_monetized = 0, advertisements will be ignored


