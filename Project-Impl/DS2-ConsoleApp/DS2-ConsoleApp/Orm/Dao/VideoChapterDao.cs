using DS2ConsoleApp.Orm.Dto;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2ConsoleApp.Orm.Dao
{
    public static class VideoChapterDao
    {
        public static string InsertVideoChapterSql = @"
            INSERT INTO [z_video_chapter] ([video_id], [title], [start_time])
            OUTPUT INSERTED.[video_id]
            VALUES (@video_id, @title, @start_time)
        ";

        public static void InsertVideoChapter(Database pDb, VideoChapter videoChapter)
        {
            var db = Database.Connect(pDb);

            var command = db.CreateCommand(InsertVideoChapterSql);
            PrepareVideoChapterInsert(command, videoChapter);
            videoChapter.ChapterId = db.ExecuteScalar<long>(command);

            Database.Close(pDb, db);
        }

        public static void PrepareVideoChapterInsert(SqlCommand command, VideoChapter videoChapter)
        {
            //@video_id, @title, @start_time
            command.Parameters.AddWithValue("@video_id", videoChapter.VideoId);
            command.Parameters.AddWithValue("@title", videoChapter.Title);
            command.Parameters.AddWithValue("@start_time", videoChapter.StartTime);
        }
    }
}
