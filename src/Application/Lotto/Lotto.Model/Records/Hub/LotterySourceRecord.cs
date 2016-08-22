using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotto.Model.Records.Hub
{
    public class LotterySourceRecord
    {
        public int Id { get; set; }

        public int LotteryId { get; set; }

        public bool IsPrimary { get; set; }

        public string DownloadUrl { get; set; }

        public virtual LotteryRecord Lottery { get; set; }
    }
}
