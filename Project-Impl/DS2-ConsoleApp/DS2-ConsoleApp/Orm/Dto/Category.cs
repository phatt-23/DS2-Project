using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2ConsoleApp.Orm.Dto
{
    public class Category
    {
        public long CategoryId { get; set; }
        public long? ParentCategoryId { get; set; }
        public string CategoryName { get; set; }

        public override string ToString()
        {
            return "Category Id: " + CategoryId + ", Parent Category Id: " + ParentCategoryId + ", Category Name: " + CategoryName;
        }
    }
}
