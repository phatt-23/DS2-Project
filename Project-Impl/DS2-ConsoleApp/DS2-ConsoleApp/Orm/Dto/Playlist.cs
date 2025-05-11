using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2ConsoleApp.Orm.Dto
{
    public class Playlist
    {
        public long PlaylistId { get; set; }
        public long? UserId { get; set; }
        public long? ChannelId { get; set; }
        public string Title { get; set; }
        public Visibility Visibility { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            return "PlaylistId: " + PlaylistId + ", UserId: " + UserId + ", ChannelId: " + ChannelId +
                   ", Title: " + Title + ", Visibility: " + Visibility.ToReprString() +
                   ", CreationDate: " + CreationDate + ", IsDeleted: " + IsDeleted;
        }
    }
}
