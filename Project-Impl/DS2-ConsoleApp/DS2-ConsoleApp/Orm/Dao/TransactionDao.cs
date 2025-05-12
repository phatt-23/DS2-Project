using DS2ConsoleApp.Orm.Dto;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DS2ConsoleApp.Orm.Dao
{
    public static class TransactionDao
    {
        public class FinishVideoUploadParameters()
        {
            public int ChannelId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public Visibility Visibility { get; set;}
            public bool IsMonetized { get; set; }
            public int Duration { get; set; }
            public long ThumbnailMediaId { get; set; }
            public long VideoMediaId { get; set; }
            public List<long> PlaylistIds { get; set; } 
            public List<long> CategoryIds { get; set; }
            public List<VideoChapter> Chapters { get; set; }
        }

        public static long FinishVideoUpload(Database pDb, FinishVideoUploadParameters parameters)
        {
            var db = Database.Connect(pDb);
            long insertedVideoId = -1;

            try
            {
                db.BeginTransaction();

                if (!ChannelDao.ChannelWithIdExists(db, parameters.ChannelId))
                    throw new Exception($"Channel with id '{parameters.ChannelId}' doesn't exist.");

                var thumbnailMedia = MediaDao.GetMediaById(db, parameters.ThumbnailMediaId)
                    ?? throw new Exception("Thumbnail media doesn't exist.");

                if (thumbnailMedia.Type != MediaType.Image)
                    throw new Exception("Expected image media type for thumbnail.");

                var videoMedia = MediaDao.GetMediaById(db, parameters.VideoMediaId)
                    ?? throw new Exception("Video media doesn't exist.");

                if (videoMedia.Type != MediaType.Video)
                    throw new Exception("Expected video media type for video.");

                var playlists = (
                    PlaylistDao.GetPlaylistsByChannel(db, parameters.ChannelId)
                ).Select(p => p.PlaylistId);

                foreach (var playlistId in parameters.PlaylistIds.Distinct())
                {
                    if (!playlists.Contains(playlistId))
                        throw new Exception($"Playlist {playlistId} doesn't belong to channel with id {parameters.ChannelId}");
                }

                foreach (var chapter in parameters.Chapters)
                {
                    if (!(chapter.StartTime >= 0 && chapter.StartTime <= parameters.Duration))
                        throw new Exception($"Chapter start time is out of bounds of the video duration. Got chapter with start time {chapter.StartTime}");
                }

                foreach (var categoryId in parameters.CategoryIds.Distinct())
                {
                    if (!CategoryDao.CategoryWithIdExists(db, categoryId))
                        throw new Exception($"Category with id {categoryId} doesn't exist.");
                }

                Video video = new ()
                {
                    ChannelId = parameters.ChannelId,
                    ThumbnailId = parameters.ThumbnailMediaId,
                    VideoFileId = parameters.VideoMediaId,
                    Visibility = parameters.Visibility,
                    IsMonetized = parameters.IsMonetized,
                    Title = parameters.Title,
                    Description = parameters.Description,
                    UploadDate = DateTime.UtcNow,
                    Duration = parameters.Duration,
                    ViewCount = 0,
                    LikeCount = 0,
                    DislikeCount = 0,
                    CommentCount = 0,
                };

                VideoDao.InsertVideo(db, video);

                foreach (var categoryId in parameters.CategoryIds.Distinct())
                {
                    VideoCategory videoCategory = new ()
                    {
                        VideoId = video.VideoId,
                        CategoryId = categoryId,
                    };

                    VideoCategoryDao.InsertVideoCategory(db, videoCategory);
                }

                foreach (var playlistId in parameters.PlaylistIds.Distinct())
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

                foreach (var chapter in parameters.Chapters)
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

        public static long SP_FinishVideoUpload(Database pDb, FinishVideoUploadParameters parameters)
        {
            var db = Database.Connect(pDb);

            using var command = db.CreateCommand("sp_finish_video_upload");
            command.CommandType = CommandType.StoredProcedure;

            // Simple parameters
            command.Parameters.AddWithValue("@p_channel_id", parameters.ChannelId);
            command.Parameters.AddWithValue("@p_title", parameters.Title);
            command.Parameters.AddWithValue("@p_description", parameters.Description);
            command.Parameters.AddWithValue("@p_visibility", parameters.Visibility.ToReprString());
            command.Parameters.AddWithValue("@p_is_monetized", parameters.IsMonetized);
            command.Parameters.AddWithValue("@p_duration", parameters.Duration);
            command.Parameters.AddWithValue("@p_thumbnail_media_id", parameters.ThumbnailMediaId);
            command.Parameters.AddWithValue("@p_video_media_id", parameters.VideoMediaId);

            // Table Valued Parameters (TVP)
            // Category IDs
            var categoryTable = new DataTable();
            categoryTable.Columns.Add("value", typeof(long));
            parameters.CategoryIds.ForEach(id => categoryTable.Rows.Add(id));

            command.Parameters.Add(new SqlParameter("@p_category_ids", SqlDbType.Structured)
            {
                TypeName = "dbo.z_bigint_list",
                Value = categoryTable
            });

            // TVP: Playlist IDs
            var playlistTable = new DataTable();
            playlistTable.Columns.Add("value", typeof(long));
            parameters.PlaylistIds.ForEach(id => playlistTable.Rows.Add(id));

            command.Parameters.Add(new SqlParameter("@p_playlist_ids", SqlDbType.Structured)
            {
                TypeName = "dbo.z_bigint_list",
                Value = playlistTable
            });

            // TVP: Chapters
            var chapterTable = new DataTable();
            chapterTable.Columns.Add("title", typeof(string));
            chapterTable.Columns.Add("start_time", typeof(int));
            parameters.Chapters.ForEach(ch => chapterTable.Rows.Add(ch.Title, ch.StartTime));

            command.Parameters.Add(new SqlParameter("@p_chapters", SqlDbType.Structured)
            {
                TypeName = "dbo.z_chapter_list",
                Value = chapterTable
            });

            // Output parameter
            var outputParam = new SqlParameter("@p_inserted_video_id", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };

            command.Parameters.Add(outputParam);

            command.ExecuteNonQuery();

            return (long)outputParam.Value;
        }
    }
}
