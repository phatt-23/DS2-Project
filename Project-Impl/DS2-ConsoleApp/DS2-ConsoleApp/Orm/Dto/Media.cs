using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2ConsoleApp.Orm.Dto
{
    public class Media
    {
        public long MediaId { get; set; }
        public string Url { get; set; }
        public MediaType Type { get; set; }
        public byte[] Data { get; set; }
        public DateTime UploadDate { get; set; }

        public override string ToString()
        {
            return "MediaId: " + MediaId + ", Url: " + Url + ", Type: " + Type.ToReprString() + ", UploadDate: " + UploadDate;
        }
    }
}
