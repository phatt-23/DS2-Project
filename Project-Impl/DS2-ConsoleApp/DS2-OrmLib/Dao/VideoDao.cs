using DS2OrmLib.Dto;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2OrmLib.Dao
{
    public class VideoDao
    {
        public static string InsertSql = @"
            INSERT INTO [z_video] (
                [channel_id], [thumbnail_id], [video_file_id], 
                [visibility], [is_monetized],
                [title], [description], [duration], 
                [view_count], [like_count], [dislike_count], [comment_count]
            ) 
            OUTPUT INSERTED.video_id
            VALUES (
                @channel_id, @thumbnail_id, @video_file_id, 
                @visibility, @is_monetized,
                @title, @description, @duration, 
                @view_count, @like_count, @dislike_count, @comment_count
            )
        ";

        public static void InsertVideo(Database pDb, Video video)
        {
            var db = Database.Connect(pDb);

            var command = db.CreateCommand(InsertSql);
            PrepareInsert(command, video);
            video.VideoId = db.ExecuteScalar<long>(command);

            Database.Close(pDb, db);
        }

        public static void PrepareInsert(SqlCommand command, Video video)
        {
            command.Parameters.AddWithValue("@channel_id", video.ChannelId); 
            command.Parameters.AddWithValue("@thumbnail_id", video.ThumbnailId); 
            command.Parameters.AddWithValue("@video_file_id", video.VideoFileId); 
            command.Parameters.AddWithValue("@visibility", video.Visibility.ToReprString()); 
            command.Parameters.AddWithValue("@is_monetized", video.IsMonetized);
            command.Parameters.AddWithValue("@title", video.Title); 
            command.Parameters.AddWithValue("@description", (object?)video.Description ?? DBNull.Value); 
            command.Parameters.AddWithValue("@duration", video.Duration); 
            command.Parameters.AddWithValue("@view_count", video.ViewCount); 
            command.Parameters.AddWithValue("@like_count", video.LikeCount); 
            command.Parameters.AddWithValue("@dislike_count", video.DislikeCount);
            command.Parameters.AddWithValue("@comment_count", video.CommentCount);        
        }
    }
}
