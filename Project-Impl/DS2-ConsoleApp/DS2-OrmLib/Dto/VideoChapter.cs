using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2OrmLib.Dto
{
    public class VideoChapter
    {
        public long ChapterId { get; set; }
        public long VideoId { get; set; }
        public string Title { get; set; }
        public int StartTime { get; set; }

        public override string ToString()
        {
            return "Chapter Id: " + ChapterId + ", Video Id: " + VideoId + ", Title: " + Title + ", Start Time: " + StartTime;
        }
    }
}
