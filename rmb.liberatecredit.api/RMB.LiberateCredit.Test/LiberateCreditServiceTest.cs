using FluentAssertions;
using NUnit.Framework;
using RMB.LiberateCredit.Domain.BindingModel;
using RMB.LiberateCredit.Domain.Enums;
using RMB.LiberateCredit.Domain.Interfaces.Handlers;
using RMB.LiberateCredit.Domain.Services;
using RMB.LiberateCredit.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace RMB.LiberateCredit.Test
{
    [TestFixture]
    public class LiberateCreditServiceTest
    {
        private static LiberateCreditService _liberateCreditService;
        private static INotificationHandler _notificationHandler;

        [SetUp]
        public void SetUp()
        {
            _notificationHandler = new NotificationHandler()
            {
                Notifications = new List<string>()
            };

            _liberateCreditService = new LiberateCreditService();
            _liberateCreditService.SetNotificationHandler(_notificationHandler);
        }

        [Test]
        public void ShouldFailValidateLimitValueCredit()
        {
            LiberateCreditBindingModel liberateCreditBindingModel = new LiberateCreditBindingModel();
            liberateCreditBindingModel.CreditValue = 99999999;

            var result = _liberateCreditService.Liberate(liberateCreditBindingModel);
            result.Should().BeNull();
            _notificationHandler.Notifications.FirstOrDefault().Should().Be("Limite máximo excedido");
        }

        [Test]
        public void ShouldFailValidateEnumClientType()
        {
            LiberateCreditBindingModel liberateCreditBindingModel = new LiberateCreditBindingModel();
            liberateCreditBindingModel.CreditValue = 1000;
            liberateCreditBindingModel.ClientType = null;

            var result = _liberateCreditService.Liberate(liberateCreditBindingModel);
            result.Should().BeNull();
            _notificationHandler.Notifications.FirstOrDefault().Should().Be("Tipo de pessoa não encontrado");
        }

        [Test]
        public void ShouldFailValidateEnumCreditType()
        {
            LiberateCreditBindingModel liberateCreditBindingModel = new LiberateCreditBindingModel();
            liberateCreditBindingModel.CreditValue = 1000;
            liberateCreditBindingModel.ClientType = ClientTypeEnum.PF;
            liberateCreditBindingModel.CreditType = null;

            var result = _liberateCreditService.Liberate(liberateCreditBindingModel);
            result.Should().BeNull();
            _notificationHandler.Notifications.FirstOrDefault().Should().Be("Tipo de crédito não encontrado");
        }

        [Test]
        public void ShouldFailValidateLimitMinPJ()
        {
            LiberateCreditBindingModel liberateCreditBindingModel = new LiberateCreditBindingModel();
            liberateCreditBindingModel.CreditValue = 1000;
            liberateCreditBindingModel.ClientType = ClientTypeEnum.PJ;
            liberateCreditBindingModel.CreditType = CreditTypeEnum.CONSIGNED_CREDIT;

            var result = _liberateCreditService.Liberate(liberateCreditBindingModel);
            result.Should().BeNull();
            _notificationHandler.Notifications.FirstOrDefault().Should().Be("Limite mínimo para pessoa júridica  não alcançado");
        }

        [Test]
        public void ShouldFailValidateParcelQuantity()
        {
            LiberateCreditBindingModel liberateCreditBindingModel = new LiberateCreditBindingModel();
            liberateCreditBindingModel.CreditValue = 1000;
            liberateCreditBindingModel.ClientType = ClientTypeEnum.PF;
            liberateCreditBindingModel.CreditType = CreditTypeEnum.CONSIGNED_CREDIT;
            liberateCreditBindingModel.QuantityParcel = 100;

            var result = _liberateCreditService.Liberate(liberateCreditBindingModel);
            result.Should().BeNull();
            _notificationHandler.Notifications.FirstOrDefault().Should().Be("Quantidade de parcelas não pode ser menor que 5 ou maior que 72");
        }

        [Test]
        public void ShouldFailValidateDueDate()
        {
            LiberateCreditBindingModel liberateCreditBindingModel = new LiberateCreditBindingModel();
            liberateCreditBindingModel.CreditValue = 1000;
            liberateCreditBindingModel.ClientType = ClientTypeEnum.PF;
            liberateCreditBindingModel.CreditType = CreditTypeEnum.CONSIGNED_CREDIT;
            liberateCreditBindingModel.QuantityParcel = 12;

            var result = _liberateCreditService.Liberate(liberateCreditBindingModel);
            result.Should().BeNull();
            _notificationHandler.Notifications.FirstOrDefault().Should().Be("Data de vencimento da primeira fatura só pode estar entre 15 dias após data atual ou até 40 dia depois da data atual");
        }

        [Test]
        public void ShouldSucessLiberateCredit()
        {
            LiberateCreditBindingModel liberateCreditBindingModel = new LiberateCreditBindingModel();
            liberateCreditBindingModel.ClientType = ClientTypeEnum.PF;
            liberateCreditBindingModel.CreditType = CreditTypeEnum.CONSIGNED_CREDIT;
            liberateCreditBindingModel.CreditValue = 10000;
            liberateCreditBindingModel.DateFirstDueDate = DateTime.Now.AddDays(15);
            liberateCreditBindingModel.QuantityParcel = 10;

            LiberateCreditViewModel liberateCreditViewModel = new LiberateCreditViewModel();
            liberateCreditViewModel.InterestValue = 1000;
            liberateCreditViewModel.TotalValue = 11000;
            liberateCreditViewModel.StatusType = StatusTypeEnum.Approved;

            var result = _liberateCreditService.Liberate(liberateCreditBindingModel);
            result.InterestValue.Should().Be(liberateCreditViewModel.InterestValue);
            result.TotalValue.Should().Be(liberateCreditViewModel.TotalValue);
            result.StatusType.Should().Be(liberateCreditViewModel.StatusType);
        }
    }
}
