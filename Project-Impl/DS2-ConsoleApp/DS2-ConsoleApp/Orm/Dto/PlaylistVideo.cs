using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS2ConsoleApp.Orm.Dto
{
    public class PlaylistVideo
    {
        public long PlaylistId { get; set; }
        public long VideoId { get; set; }
        public DateTime AddedDate { get; set; }
        public int Order { get; set; }
    }
}
