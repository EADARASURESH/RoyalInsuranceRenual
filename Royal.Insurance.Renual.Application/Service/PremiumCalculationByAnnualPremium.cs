using Royal.Insurance.Renual.DTO;
using System;
using System.Collections.Generic;

namespace Royal.Insurance.Renual.Application.Service
{
    public class PremiumCalculationByAnnualPremium : IPremiumCalculation
    {
        public List<OutPutDTO> PremiumCalculationAmount(List<InputDTO> inputDtOs)
        {
            var outPutDtOs = new List<OutPutDTO>();
            foreach (var inputDto in inputDtOs)
            {
                var outPutDto = new OutPutDTO();
                outPutDto.CreditCharge = (5 * inputDto.AnnualPemium) / 100;
                outPutDto.TotalPremium = inputDto.AnnualPemium + outPutDto.CreditCharge;
                double divideAverageAmount = outPutDto.TotalPremium / 12;
                double monthlyAmount = Math.Round(divideAverageAmount, 2);
                double monthlyAmountExcess = Math.Round(divideAverageAmount, 10);
                double exceedAmount = Math.Round((monthlyAmountExcess - monthlyAmount) * 12, 2);
                outPutDto.InitialMonthlyPaymentAmount = monthlyAmount + exceedAmount;
                outPutDto.OtherMonthlyPaymentsAmount = monthlyAmount;
                outPutDto.AnnualPemium = inputDto.AnnualPemium;
                outPutDto.CustomerId = inputDto.CustomerId;
                outPutDto.FirstName = inputDto.FirstName;
                outPutDto.PayOutAmount = inputDto.PayOutAmount;
                outPutDto.ProductName = inputDto.ProductName;
                outPutDto.Surname = inputDto.Surname;
                outPutDto.Title = inputDto.Title;
                outPutDtOs.Add(outPutDto);
            }
            return outPutDtOs;

        }
    }
}
