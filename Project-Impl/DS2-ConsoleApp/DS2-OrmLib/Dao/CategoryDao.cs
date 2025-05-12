using DS2ConsoleApp.Helper;
using DS2OrmLib.Dto;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2OrmLib.Dao
{
    public class CategoryDao
    {
        public static string SelectByNameSql = @"
            SELECT *
            FROM z_category
            WHERE name LIKE @search_name
        ";

        public static string GetCategoryByIdSql = @"
            SELECT * FROM z_category WHERE category_id = @category_id
        ";

        public static bool CategoryWithIdExists(Database pDb, long categoryId)
        {
            return GetCategoryById(pDb, categoryId) != null;
        }

        public static Category? GetCategoryById(Database pDb, long categoryId)
        {
            var db = Database.Connect(pDb);

            var command = db.CreateCommand(GetCategoryByIdSql);
            command.Parameters.AddWithValue("@category_id", categoryId);
            using var reader = db.Select(command);

            Category? category = reader.Read() ? MapReaderToCategory(reader) : null;
             
            Database.Close(pDb, db);
            return category;
        }

        public static List<Category> GetCategoriesByName(Database pDb, string searchName)
        {
            var db = Database.Connect(pDb);

            var command = db.CreateCommand(SelectByNameSql);
            command.Parameters.AddWithValue("@search_name", $"%{searchName}%");

            using var reader = db.Select(command);

            List<Category> categories = [];
            while (reader.Read())
            {
                var category = MapReaderToCategory(reader);
                categories.Add(category);
            }

            Database.Close(pDb, db);
            return categories;
        }

        public static Category MapReaderToCategory(SqlDataReader reader)
        {
            return new Category()
            {
                CategoryId = SqlAttribMapper.Get<long>(reader, "category_id"),
                ParentCategoryId = SqlAttribMapper.GetNullable<long>(reader, "parent_category_id"),
                CategoryName = SqlAttribMapper.GetString(reader, "category_name"),
            };
        }
    }
}
