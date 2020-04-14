using RMB.LiberateCredit.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMB.LiberateCredit.Domain.BindingModel
{
    public class LiberateCreditBindingModel
    {
        public decimal CreditValue { get; set; }
        public CreditTypeEnum CreditType { get; set; }
        public ClientTypeEnum ClientType { get; set; }
        public int QuantityParcel { get; set; }
        public DateTime DateFirstDueDate { get; set; }
    }
}
