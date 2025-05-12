using DS2OrmLib.Dto;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS2OrmLib.Dao
{
    public static class ChannelDao
    {
        public static string GetChannelsByUserCommand = @"
            SELECT * FROM z_channel 
            WHERE user_id = @user_id   
        ";

        public static string GetChannelByIdCommand = @"
            SELECT * FROM z_channel WHERE channel_id = @channel_id
        ";

        public static bool ChannelWithIdExists(Database pDb, long channelId)
        {
            return GetChannelById(pDb, channelId) != null;
        }

        public static Channel? GetChannelById(Database pDb, long channelId)
        {
            var db = Database.Connect(pDb);

            var command = db.CreateCommand(GetChannelByIdCommand);
            command.Parameters.AddWithValue("@channel_id", channelId);

            using var reader = db.Select(command);

            Channel? channel = reader.Read()
                ? MapRecordToChannel(reader)
                : null;

            Database.Close(pDb, db);
            return channel;
        }

        public static List<Channel> GetChannelsByUser(Database pDb, long userId)
        {
            var db = Database.Connect(pDb);

            var command = db.CreateCommand(GetChannelsByUserCommand);
            command.Parameters.AddWithValue("@user_id", userId);

            using var reader = db.Select(command);

            List<Channel> channels = [];
            while (reader.Read())
            {
                var channel = MapRecordToChannel(reader);
                channels.Add(channel);
            }

            Database.Close(pDb, db);
            return channels;
        }

        public static Channel MapRecordToChannel(SqlDataReader reader)
        {
            var channelId = reader.GetInt64(reader.GetOrdinal("channel_id"));
            var userId = reader.GetInt64(reader.GetOrdinal("user_id"));
            var channelName = reader.GetString(reader.GetOrdinal("channel_name"));

            string? description = reader.IsDBNull(reader.GetOrdinal("description"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("description"));

            long? pfpMediaId = reader.IsDBNull(reader.GetOrdinal("pfp_media_id"))
                    ? null
                    : reader.GetInt64(reader.GetOrdinal("pfp_media_id"));

            long? bannerMediaId = reader.IsDBNull(reader.GetOrdinal("banner_media_id"))
                    ? null
                    : reader.GetInt64(reader.GetOrdinal("pfp_media_id"));

            var creationDate = reader.GetDateTime(reader.GetOrdinal("creation_date"));
            var isDeleted = reader.GetBoolean(reader.GetOrdinal("is_deleted"));

            return new Channel()
            {
                ChannelId = channelId,
                UserId = userId,
                ChannelName = channelName,
                Description = description,
                PfpMediaId = pfpMediaId,
                BannerMediaId = bannerMediaId,
                CreationDate = creationDate,
                IsDeleted = isDeleted,
            };
        }
    }
}
