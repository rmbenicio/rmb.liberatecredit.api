using RMB.LiberateCredit.Domain.Interfaces.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMB.LiberateCredit.Domain.ViewModel
{
    public class NotificationHandler: INotificationHandler
    {
        public List<string> Notifications { get; set; }
    }
}
