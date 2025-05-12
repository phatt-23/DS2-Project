using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2OrmLib.Dto
{
    public class Channel
    {
        public long ChannelId { get; set; }
        public long UserId { get; set; }
        public string ChannelName { get; set; }
        public string? Description { get; set; }
        public long? PfpMediaId { get; set; }
        public long? BannerMediaId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDeleted { get; set; }

        public override string ToString()
        {
            return "ChannelId: " + ChannelId + ", UserId: " + UserId + ", ChannelName: " + ChannelName +
                   ", Description: " + Description + ", PfpMediaId: " + PfpMediaId +
                   ", BannerMediaId: " + BannerMediaId + ", CreationDate: " + CreationDate +
                   ", IsDeleted: " + IsDeleted;
        }
    }
}
