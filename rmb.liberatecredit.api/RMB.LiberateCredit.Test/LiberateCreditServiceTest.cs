using FluentAssertions;
using NUnit.Framework;
using RMB.LiberateCredit.Domain.BindingModel;
using RMB.LiberateCredit.Domain.Enums;
using RMB.LiberateCredit.Domain.Services;
using RMB.LiberateCredit.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMB.LiberateCredit.Test
{
    [TestFixture]
    public class LiberateCreditServiceTest
    {
        private static LiberateCreditService liberateCreditService;

        [SetUp]
        public void SetUp()
        {
            liberateCreditService = new LiberateCreditService();
        }

        [Test]
        public void Should_Be_Sucess()
        {

            LiberateCreditBindingModel bindingmodel = new LiberateCreditBindingModel();

            bindingmodel.ClientType = ClientTypeEnum.PF;
            bindingmodel.CreditType = CreditTypeEnum.CONSIGNED_CREDIT;
            bindingmodel.CreditValue = 10000;
            bindingmodel.DateFirstDueDate = DateTime.Now.AddDays(15);
            bindingmodel.QuantityParcel = 10;
            var result = liberateCreditService.Liberate(bindingmodel);

            LiberateCreditViewModel returnLiberateCredit = new LiberateCreditViewModel();
            returnLiberateCredit.InterestValue = 1000;
            returnLiberateCredit.TotalValue = 11000;
            returnLiberateCredit.StatusType = StatusTypeEnum.Approved;
            result.Should().Be(returnLiberateCredit);
        }
    }
}
