using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBApi;
using MDA.Enum;
using MDA.Model;
using Newtonsoft.Json;

namespace MDA.Implementation
{
    public partial class IBClient : EWrapper
    {
        public event EventHandler<IBNotificationModel> NotificationReceived;

        void EWrapper.error(Exception e)
        {
            IBNotificationModel ibNotification = new IBNotificationModel
            {
                errorMsg = e.Message,
                type = NotificationType.Error
            };
            NotificationReceived?.Invoke(this, ibNotification);
        }

        void EWrapper.error(string str)
        {
            IBNotificationModel ibNotification = new IBNotificationModel
            {
                errorMsg = str,
                type = NotificationType.Error
            };
            NotificationReceived?.Invoke(this, ibNotification);
        }

        void EWrapper.error(int id, int errorCode, string errorMsg, string advancedOrderRejectJson)
        {
            IBNotificationModel ibNotification = new IBNotificationModel
            {
                id = id,
                errorCode = errorCode,
                errorMsg = errorMsg,
                advancedOrderRejectJson = advancedOrderRejectJson,
                type = errorCode switch
                { 
                    2104 or 2017 or 2157 => NotificationType.OK,
                    _ => NotificationType.Error
                }
            };
            NotificationReceived?.Invoke(this, ibNotification);
        }
    }
}
