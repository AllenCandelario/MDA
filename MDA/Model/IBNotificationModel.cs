using MDA.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDA.Model
{
    public class IBNotificationModel
    {
        public int id { get; set; }
        public int errorCode { get; set; }
        public string errorMsg { get; set; }
        public string advancedOrderRejectJson { get; set; }
        public NotificationType type { get; set; }
    }
}
