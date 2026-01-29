using MDA.Enum;

/*
    DECISIONS:
    - Use of class (sealed + record) because of the strings requirement for error messages
    - We use 1 model to capture all the different event types instead of an abstract record like IBAccountUpdate because it's generic enough

    FUTURE IMPROVEMENTS:
*/

namespace MDA.Model
{
    public sealed record IBNotification
    {
        public IBNotification(int id, int errorCode, string errorMsg, string advancedOrderRejectJson)
        {
            this.Id = id;
            this.ErrorCode = errorCode;
            this.ErrorMsg = errorMsg;
            this.AdvancedOrderRejectJson = advancedOrderRejectJson;
            this.Type = errorCode switch
            {
                2104 or 2017 or 2157 => NotificationType.OK,
                _ => NotificationType.Error
            };
        }

        public IBNotification(string errorMsg, NotificationType type = NotificationType.Error)
        {
            this.ErrorMsg = errorMsg;
            this.Type = type;
        }

        public int Id { get; }
        public int ErrorCode { get; }
        public string? ErrorMsg { get; }
        public string? AdvancedOrderRejectJson { get; }
        public NotificationType Type { get; }
    }
}
