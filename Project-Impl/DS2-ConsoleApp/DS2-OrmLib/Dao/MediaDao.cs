using DS2ConsoleApp.Helper;
using DS2OrmLib.Dto;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DS2OrmLib.Dao
{
    public class MediaDao
    {
        public static string InsertSql = @"
            INSERT INTO [z_media] ([url], [type], [data], [upload_date])
            OUTPUT                INSERTED.[media_id]
            VALUES                (@url,  @type,  @data,  @upload_date)
        ";

        public static string GetMediaByid = @"
            SELECT * FROM z_media WHERE media_id = @media_id
        ";

        public static Media? GetMediaById(Database pDb, long mediaId)
        {
            var db = Database.Connect(pDb);

            var command = db.CreateCommand(GetMediaByid);
            command.Parameters.AddWithValue("@media_id", mediaId);
            using var reader = db.Select(command);

            Media? media = reader.Read() ? MapReaderToMedia(reader) : null;

            Database.Close(pDb, db);
            return media;
        }

        public static void InsertMedia(Database pDb, Media media)
        {
            var db = Database.Connect(pDb);

            var command = db.CreateCommand(InsertSql);
            PrepareInsert(command, media);
            media.MediaId = db.ExecuteScalar<long>(command);

            Database.Close(pDb, db);
        }

        public static Media MapReaderToMedia(SqlDataReader reader)
        {
            return new Media()
            {
                MediaId = SqlAttribMapper.Get<long>(reader, "media_id"),
                Url = SqlAttribMapper.GetString(reader, "url"),
                Type = SqlAttribMapper.GetString(reader, "type").ToMediaTypeEnum(),
                Data = SqlAttribMapper.GetBinary(reader, "data"),
                UploadDate = SqlAttribMapper.Get<DateTime>(reader, "upload_date"),
            };
        }

        public static void PrepareInsert(SqlCommand command, Media media)
        {
            command.Parameters.AddWithValue("@url", media.Url);
            command.Parameters.AddWithValue("@type", media.Type.ToReprString());
            command.Parameters.AddWithValue("@data", media.Data);
            command.Parameters.AddWithValue("@upload_date", media.UploadDate);
        }
    }
}
