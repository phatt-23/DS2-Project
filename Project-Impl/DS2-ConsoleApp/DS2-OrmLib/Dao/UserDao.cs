using DS2OrmLib.Dto;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2OrmLib.Dao
{
    public static class UserDao
    {
        public static string SelectAllUsers = "SELECT * FROM z_user";
        public static string SelectUserById = "SELECT * FROM z_user WHERE user_id = @user_id";
        public static string SelectUserByName = @"
            SELECT * FROM z_user 
            WHERE lower(username) LIKE @search_name 
               OR lower(first_name) LIKE @search_name 
               OR lower(last_name) LIKE @search_name 
        ";

        public static List<User> GetAllUsers(Database pDb)
        {
            var db = Database.Connect(pDb);

            var command = db.CreateCommand(SelectAllUsers);
            using var reader = db.Select(command);

            List<User> users = [];
            while (reader.Read())
            {
                var user = MapRecordToUser(reader);
                users.Add(user);
            }

            Database.Close(pDb, db);
            return users;
        }

        public static List<User> GetUsersByName(Database pDb, string name)
        {
            var db = Database.Connect(pDb);

            var command = db.CreateCommand(SelectUserByName);
            command.Parameters.AddWithValue("@search_name", name.ToLower());

            using var reader = db.Select(command);

            List<User> users = [];
            while (reader.Read())
            {
                var user = MapRecordToUser(reader);
                users.Add(user);
            }

            Database.Close(pDb, db);
            return users;
        }

        public static User MapRecordToUser(SqlDataReader reader)
        {
            var userId = reader.GetInt64(reader.GetOrdinal("user_id"));
            var username = reader.GetString(reader.GetOrdinal("username"));
            var firstname = reader.GetString(reader.GetOrdinal("first_name"));
            var lastname = reader.GetString(reader.GetOrdinal("last_name"));
            var email = reader.GetString(reader.GetOrdinal("email"));
            var regDate = reader.GetDateTime(reader.GetOrdinal("registration_date"));

            var aboutMe = reader.IsDBNull(reader.GetOrdinal("about_me"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("about_me"));

            long? pfpId =
                reader.IsDBNull(reader.GetOrdinal("profile_picture_id"))
                    ? null
                    : reader.GetInt64(reader.GetOrdinal("profile_picture_id"));

            DateTime? lastLogin = reader.IsDBNull(reader.GetOrdinal("last_login"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("last_login"));

            var status = reader.GetString(reader.GetOrdinal("status")).ToUserStatusEnum();

            var user = new User
            {
                UserId = userId,
                Username = username,
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                RegistrationDate = regDate,
                AboutMe = aboutMe,
                ProfilePictureId = pfpId,
                LastLogin = lastLogin,
                Status = status,
            };

            return user;
        }
    }
}
