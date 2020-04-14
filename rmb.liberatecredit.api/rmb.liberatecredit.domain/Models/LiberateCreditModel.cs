using RMB.LiberateCredit.Domain.BindingModel;
using RMB.LiberateCredit.Domain.Enums;
using RMB.LiberateCredit.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMB.LiberateCredit.Domain.Models
{
    public class LiberateCreditModel
    {
        public LiberateCreditModel(LiberateCreditBindingModel liberateCreditBindingModel)
        {
            CreditValue = liberateCreditBindingModel.CreditValue;
            CreditType = liberateCreditBindingModel.CreditType;
            ClientType = liberateCreditBindingModel.ClientType;
            QuantityParcel = liberateCreditBindingModel.QuantityParcel;
            DateFirstDueDate = liberateCreditBindingModel.DateFirstDueDate;
        }

        public decimal CreditValue { get; set; }
        public CreditTypeEnum CreditType { get; set; }
        public ClientTypeEnum ClientType { get; set; }
        public int QuantityParcel { get; set; }
        public DateTime DateFirstDueDate { get; set; }
        public int Id { get; set; }


        public bool ValidateLimitValueCredit()
        {
            bool validateReturn = false;
            if (CreditValue <= 1000000)
            {
                validateReturn = true;
            }
            return validateReturn;
        }

        public bool ValidateParcelQuantity()
        {
            bool validateReturn = false;
            if (5 >= QuantityParcel || QuantityParcel <= 72)
            {
                validateReturn = true;
            }
            return validateReturn;
        }

        public bool ValidateEnumCreditType()
        {
            bool validateReturn = false;
            bool success = Enum.IsDefined(typeof(CreditTypeEnum), CreditType);

            return validateReturn;
        }

        public bool ValidateEnumClientType()
        {
            bool validateReturn = false;
            bool success = Enum.IsDefined(typeof(ClientTypeEnum), ClientType);

            return validateReturn;
        }

        public bool ValidateLimitMinPJ()
        {
            bool validateReturn = true;
            if (ClientType == ClientTypeEnum.PJ)
            {
                validateReturn = false;
                if (CreditValue >= 15000)
                {
                    validateReturn = true;

                }
            }
            return validateReturn;
        }

        public bool ValidateDueDate()
        {
            bool validateReturn = false;
            DateTime datemin = DateTime.Now.AddDays(+15);
            DateTime datemax = DateTime.Now.AddDays(40);
            if (datemin.Date >= DateFirstDueDate.Date || DateFirstDueDate.Date <= datemax.Date)
            {

                validateReturn = true;


            }
            return validateReturn;
        }

        public LiberateCreditViewModel LiberateCreditViewModel(bool statusCredit)
        {
            LiberateCreditViewModel returnLiberateCredit = new LiberateCreditViewModel();

            if (statusCredit)
            {
                returnLiberateCredit.InterestValue = 0;
                returnLiberateCredit.TotalValue = 0;
                returnLiberateCredit.StatusType = StatusTypeEnum.Recused;
            }
            else
            {
                int value = Convert.ToInt32(CreditType);
                double percent =  (Convert.ToDouble(value)/100.00);

                returnLiberateCredit.InterestValue = Convert.ToDecimal( (QuantityParcel * percent) * Convert.ToDouble(CreditValue) );
                returnLiberateCredit.TotalValue = Convert.ToDecimal((QuantityParcel * percent) * Convert.ToDouble(CreditValue) + Convert.ToDouble(CreditValue)); ;
                returnLiberateCredit.StatusType = StatusTypeEnum.Approved;
            }


            return returnLiberateCredit;

        }
    }

}
