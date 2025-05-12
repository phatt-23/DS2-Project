using DS2OrmLib.Dto;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2OrmLib.Dao
{
    public static class PlaylistVideoDao
    {
        public static string InsertPlaylistVideoSql = @"
            INSERT INTO z_playlist_video (playlist_id, video_id, [order])
            VALUES (@playlist_id, @video_id, @order)
        ";

        public static void InsertPlaylistVideo(Database pDb, PlaylistVideo playlistVideo)
        {
            var db = Database.Connect(pDb);

            var command = db.CreateCommand(InsertPlaylistVideoSql);
            PrepareInsert(command, playlistVideo);
            db.ExecuteNonQuery(command);

            Database.Close(pDb, db);
        }

        public static void PrepareInsert(SqlCommand command, PlaylistVideo playlistVideo)
        {
            command.Parameters.AddWithValue("@playlist_id", playlistVideo.PlaylistId);
            command.Parameters.AddWithValue("@video_id", playlistVideo.VideoId);
            command.Parameters.AddWithValue("@order", playlistVideo.Order);
        }
    }
}
