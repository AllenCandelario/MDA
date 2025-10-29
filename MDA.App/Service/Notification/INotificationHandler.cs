using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MDA.Model;

namespace MDA.App.Service.Notification
{
    public interface INotificationHandler
    {
        Task HandleAsync(IBNotification notification, CancellationToken cancellationToken);
    }
}
