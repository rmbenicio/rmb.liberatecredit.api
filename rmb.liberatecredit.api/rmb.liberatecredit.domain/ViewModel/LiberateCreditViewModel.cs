using RMB.LiberateCredit.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RMB.LiberateCredit.Domain.ViewModel
{
    public class LiberateCreditViewModel
    {
        public decimal TotalValue { get; set; }
        public StatusTypeEnum StatusType { get; set; }
        public decimal InterestValue { get; set; }
    }
}
