using DS2ConsoleApp.Orm.Dto;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2ConsoleApp.Orm.Dao
{
    public class VideoCategoryDao
    {
        public static string InsertVideoCategorySql = @"
            INSERT INTO z_video_category (video_id, category_id)
            VALUES (@video_id, @category_id)
        ";

        public static void InsertVideoCategory(Database pDb, VideoCategory videoCategory)
        {
            var db = Database.Connect(pDb);

            var command = db.CreateCommand(InsertVideoCategorySql);
            PrepareInsert(command, videoCategory);
            db.ExecuteNonQuery(command);

            Database.Close(pDb, db);
        }

        public static void PrepareInsert(SqlCommand command, VideoCategory videoCategory)
        {
            command.Parameters.AddWithValue("@video_id", videoCategory.VideoId);
            command.Parameters.AddWithValue("@category_id", videoCategory.CategoryId);
        }
    }
}
