using DS2ConsoleApp.Helper;
using DS2OrmLib.Dto;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;

namespace DS2OrmLib.Dao
{
    public static class PlaylistDao
    {
        public static string InsertSql = @"
            INSERT INTO [z_playlist] ([user_id], [channel_id], [title], [visibility], [creation_date])
            OUTPUT                   INSERTED.[playlist_id]
            VALUES                   (@user_id,  @channel_id,  @title,  @visibility,  @creation_date)
        ";

        public static string GetPlaylistsByChannelSql = @"
            SELECT  *
            FROM    [z_playlist]
            WHERE   [channel_id] = @channel_id
        ";

        public static string GetNextOrderSql = @"
            SELECT COALESCE(MAX(pv.[order]), 0) + 1
            FROM z_playlist_video pv
            WHERE playlist_id = @playlist_id
        ";

        public static int GetNextOrder(Database pDb, long playlistId)
        {
            var db = Database.Connect(pDb);

            var command = db.CreateCommand(GetNextOrderSql);
            command.Parameters.AddWithValue("@playlist_id", playlistId);
            var nextOrder = db.ExecuteScalar<int>(command);

            Database.Close(pDb, db);
            return (int)Convert.ChangeType(nextOrder, typeof(int));
        }

        public static void InsertPlaylist(Database pDb, Playlist playlist)
        {
            var db = Database.Connect(pDb);

            var command = db.CreateCommand(InsertSql);
            PrepareInsert(command, playlist);
            playlist.PlaylistId = db.ExecuteScalar<long>(command);

            Database.Close(pDb, db);
        }

        public static List<Playlist> GetPlaylistsByChannel(Database pDb, long channelId)
        {
            var db = Database.Connect(pDb);

            var command = db.CreateCommand(GetPlaylistsByChannelSql);
            command.Parameters.AddWithValue("@channel_id", channelId);
            using var reader = db.Select(command);

            List<Playlist> playlists = [];
            while (reader.Read())
            {
                var playlist = MapReaderToPlaylist(reader);
                playlists.Add(playlist);
            }

            Database.Close(pDb, db);
            return playlists;
        }

        public static Playlist MapReaderToPlaylist(SqlDataReader reader)
        {
            var playlistId = SqlAttribMapper.Get<long>(reader, "playlist_id");
            var userId = SqlAttribMapper.GetNullable<long>(reader, "user_id");
            var channelId = SqlAttribMapper.GetNullable<long>(reader, "channel_id");
            var title = SqlAttribMapper.GetString(reader, "title");
            var visibility = SqlAttribMapper.GetVisibility(reader, "visibility");
            var creationDate = SqlAttribMapper.Get<DateTime>(reader, "creation_date");
            var isDeleted = SqlAttribMapper.Get<bool>(reader, "playlist_id");

            return new Playlist()
            {
                PlaylistId = playlistId,
                UserId = userId,
                ChannelId = channelId,
                Title = title,
                Visibility = visibility,
                CreationDate = creationDate,
                IsDeleted = isDeleted
            };
        }

        public static void PrepareInsert(SqlCommand command, Playlist playlist)
        {
            command.Parameters.AddWithValue("@user_id", (object?)playlist.UserId ?? DBNull.Value);
            command.Parameters.AddWithValue("@channel_id", (object?)playlist.ChannelId ?? DBNull.Value);
            command.Parameters.AddWithValue("@title", playlist.Title);
            command.Parameters.AddWithValue("@visibility", playlist.Visibility.ToReprString());
            command.Parameters.AddWithValue("@creation_date", playlist.CreationDate);
        }
    }
}
