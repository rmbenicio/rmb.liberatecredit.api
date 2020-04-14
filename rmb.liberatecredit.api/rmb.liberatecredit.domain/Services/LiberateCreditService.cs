using RMB.LiberateCredit.Domain.BindingModel;
using RMB.LiberateCredit.Domain.Interfaces.Handlers;
using RMB.LiberateCredit.Domain.Interfaces.Services;
using RMB.LiberateCredit.Domain.Models;
using RMB.LiberateCredit.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RMB.LiberateCredit.Domain.Services
{
    public class LiberateCreditService : ILiberateCreditService
    {

        private INotificationHandler _notificationHandler;

        public void SetNotificationHandler(INotificationHandler notificationHandler)
        {
            _notificationHandler = notificationHandler;
        }
        public LiberateCreditService()
        {

        }

        public LiberateCreditViewModel Liberate(LiberateCreditBindingModel bindingmodel)
        {
            var result = new LiberateCreditViewModel();
            var model = new LiberateCreditModel(bindingmodel);
            bool statusCredit = false;
            _notificationHandler.Notifications = new List<string>();

            if (!model.ValidateLimitValueCredit())
            {
                _notificationHandler.Notifications.Add("Limite máximo excedido");
            }

            if (!model.ValidateEnumClientType())
            {
                _notificationHandler.Notifications.Add("Tipo de pessoa não encontrado");
            }

            if (!model.ValidateEnumCreditType())
            {
                _notificationHandler.Notifications.Add("Tipo de crédito não encontrado");
            }
            if (!model.ValidateLimitMinPJ())
            {
                _notificationHandler.Notifications.Add("Limite mínimo para pessoa júridica  não alcançado");
            }

            if (!model.ValidateParcelQuantity())
            {
                _notificationHandler.Notifications.Add("Quantidade de parcelas não pode ser menor que 5 ou maior que 72");
            }

            if (!model.ValidateDueDate())
            {
                _notificationHandler.Notifications.Add("Data de vencimento da primeira fatura só pode estar entre 15 dias após data atual ou até 40 dia depois da data atual");
            }

            result = model.LiberateCreditViewModel(statusCredit);

            return result;
        }
    }
}
