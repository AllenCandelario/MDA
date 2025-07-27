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
    - Using an Action event signature instead of EventHandler to manage messages/notifications --> Currently have no need for EventArgs with sender info

    FUTURE IMPROVEMENTS:
    - Use channels instead of delegates if back-pressure starts becoming a problem
    - Use an interface i.e. INotificationSink if you find yourself changing the handling of messages/notifications
    - A code path that the JIT’s tier‑1 compiler has fully optimised and inlined after a warm‑up. Run a quick synthetic load at startup so your handlers are “hot”.
*/
namespace MDA.Implementation
{
    public partial class IBClient : EWrapper
    {
        public event Action<IBNotification>? NotificationReceived;

        void EWrapper.error(Exception e)
        {
            IBNotification ibNotification = new IBNotification(e.Message);
            NotificationReceived?.Invoke(ibNotification);
        }

        void EWrapper.error(string str)
        {
            IBNotification ibNotification = new IBNotification(str);
            NotificationReceived?.Invoke(ibNotification);
        }

        // EWrapper method but we're benchmarking this so we cannot use the explicit method interface implementation (without casting it)
        public void error(int id, int errorCode, string errorMsg, string advancedOrderRejectJson)
        {
            IBNotification ibNotification = new IBNotification(id, errorCode, errorMsg, advancedOrderRejectJson);
            NotificationReceived?.Invoke(ibNotification);
        }
    }
}
