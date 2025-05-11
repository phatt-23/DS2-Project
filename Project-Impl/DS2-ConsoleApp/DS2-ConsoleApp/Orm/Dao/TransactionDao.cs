using DS2ConsoleApp.Orm.Dto;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2ConsoleApp.Orm.Dao
{
    public static class TransactionDao
    {
        public static long FinishVideoUpload(
            Database pDb,
            int channelId,
            string title,
            string description,
            Visibility visibility,
            bool isMonetized,
            int duration,
            long thumbnailMediaId,
            long videoMediaId,
            List<long> playlistIds,
            List<long> categoryIds,
            List<VideoChapter> chapters
            )
        {
            var db = Database.Connect(pDb);
            long insertedVideoId = -1;

            try
            {
                db.BeginTransaction();

                if (!ChannelDao.ChannelWithIdExists(db, channelId))
                    throw new Exception($"Channel with id '{channelId}' doesn't exist.");

                var thumbnailMedia = MediaDao.GetMediaById(db, thumbnailMediaId)
                    ?? throw new Exception("Thumbnail media doesn't exist.");

                if (thumbnailMedia.Type != MediaType.Image)
                    throw new Exception("Expected image media type for thumbnail.");

                var videoMedia = MediaDao.GetMediaById(db, videoMediaId)
                    ?? throw new Exception("Video media doesn't exist.");

                if (videoMedia.Type != MediaType.Video)
                    throw new Exception("Expected video media type for video.");

                var playlists = (
                    PlaylistDao.GetPlaylistsByChannel(db, channelId)
                ).Select(p => p.PlaylistId);

                foreach (var playlistId in playlistIds.Distinct())
                {
                    if (!playlists.Contains(playlistId))
                        throw new Exception($"Playlist {playlistId} doesn't belong to channel with id {channelId}");
                }

                foreach (var chapter in chapters)
                {
                    if (!(chapter.StartTime >= 0 && chapter.StartTime <= duration))
                        throw new Exception($"Chapter start time is out of bounds of the video duration. Got chapter with start time {chapter.StartTime}");
                }

                foreach (var categoryId in categoryIds.Distinct())
                {
                    if (!CategoryDao.CategoryWithIdExists(db, categoryId))
                        throw new Exception($"Category with id {categoryId} doesn't exist.");
                }

                Video video = new ()
                {
                    ChannelId = channelId,
                    ThumbnailId = thumbnailMediaId,
                    VideoFileId = videoMediaId,
                    Visibility = visibility,
                    IsMonetized = isMonetized,
                    Title = title,
                    Description = description,
                    UploadDate = DateTime.UtcNow,
                    Duration = duration,
                    ViewCount = 0,
                    LikeCount = 0,
                    DislikeCount = 0,
                    CommentCount = 0,
                };

                VideoDao.InsertVideo(db, video);

                foreach (var categoryId in categoryIds.Distinct())
                {
                    VideoCategory videoCategory = new ()
                    {
                        VideoId = video.VideoId,
                        CategoryId = categoryId,
                    };

                    VideoCategoryDao.InsertVideoCategory(db, videoCategory);
                }

                foreach (var playlistId in playlistIds.Distinct())
                {
                    var nextOrder = PlaylistDao.GetNextOrder(db, playlistId);
                    PlaylistVideo playlistVideo = new()
                    {
                        PlaylistId = playlistId,
                        VideoId = video.VideoId,
                        Order = nextOrder,
                    };

                    PlaylistVideoDao.InsertPlaylistVideo(db, playlistVideo);
                }

                foreach (var chapter in chapters)
                {
                    chapter.VideoId = video.VideoId;
                    VideoChapterDao.InsertVideoChapter(db, chapter);
                }

                db.EndTransaction();
                insertedVideoId = video.VideoId;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                db.Rollback();
            }

            Database.Close(pDb, db);
            return insertedVideoId;
        }

    }
}
