using System;
using System.Collections.Generic;
using System.Text;

namespace Royal.Insurance.Renual.DTO
{
    public class OutPutDTO:InputDTO
    {
        public double TotalPremium  { get; set; }
        public double AverageMonthlyPremium { get; set; }
        public double InitialMonthlyPaymentAmount { get; set; }
        public double OtherMonthlyPaymentsAmount { get; set; }
        public double CreditCharge { get; set; }
        public string TextFile { get; set; }
    }
}
