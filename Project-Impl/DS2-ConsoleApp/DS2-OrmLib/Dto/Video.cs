using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2OrmLib.Dto
{
    public class Video
    {
        public long VideoId { get; set; }
        public long ChannelId { get; set; }
        public long ThumbnailId { get; set; }
        public long VideoFileId { get; set; }
        public Visibility Visibility { get; set; }
        public bool IsMonetized { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
        public int Duration { get; set; }
        public int ViewCount { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }
        public int CommentCount { get; set; }
    }
}
