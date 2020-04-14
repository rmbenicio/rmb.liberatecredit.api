using System;
using System.Collections.Generic;
using System.Text;

namespace RMB.LiberateCredit.Domain.Interfaces.Handlers
{
    public interface INotificationHandler
    {
        List<string> Notifications { get; set; }
    }
}
