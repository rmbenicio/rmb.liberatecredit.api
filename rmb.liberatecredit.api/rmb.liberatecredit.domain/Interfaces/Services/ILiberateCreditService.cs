using RMB.LiberateCredit.Domain.BindingModel;
using RMB.LiberateCredit.Domain.Interfaces.Handlers;
using RMB.LiberateCredit.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RMB.LiberateCredit.Domain.Interfaces.Services
{
    public interface ILiberateCreditService
    {
        LiberateCreditViewModel Liberate(LiberateCreditBindingModel model);
        void SetNotificationHandler(INotificationHandler notificationHandler);

    }
}
