using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBApi;
using MDA.Enum;
using MDA.Model;

/*
    DECISIONS:
    - Use structs > class more much higher speeds
    - Use readonly stuct > record struct to maintain flexibility for own method implementations like .Equals or .ToString if needed
    - Using an event handler to manage messages/notifications

    FUTURE IMPROVEMENTS:
    - Use channels instead of delegates if back-pressure starts becoming a problem
    - Use an interface i.e. INotificationSink if you find yourself changing the handling of messages/notifications
    - A code path that the JIT’s tier‑1 compiler has fully optimised and inlined after a warm‑up. Run a quick synthetic load at startup so your handlers are “hot”.
*/
namespace MDA.Implementation
{
    public partial class IBClient : EWrapper
    {
        public event EventHandler<IBNotification> NotificationReceived;

        void EWrapper.error(Exception e)
        {
            IBNotification ibNotification = new IBNotification(e.Message);
            NotificationReceived?.Invoke(this, ibNotification);
        }

        void EWrapper.error(string str)
        {
            IBNotification ibNotification = new IBNotification(str);
            NotificationReceived?.Invoke(this, ibNotification);
        }

        public void error(int id, int errorCode, string errorMsg, string advancedOrderRejectJson)
        {
            IBNotification ibNotification = new IBNotification(id, errorCode, errorMsg, advancedOrderRejectJson);
            NotificationReceived?.Invoke(this, ibNotification);
        }
    }
}
