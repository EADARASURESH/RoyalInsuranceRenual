using Royal.Insurance.Renual.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Royal.Insurance.Renual.Application.Service
{
    public  class PremiumCalculation
    {
        public  List<OutPutDTO> PremiumCalculationAmount(List<InputDTO> inputDTOs)
        {
            var outPutDTOs = new List<OutPutDTO>();
            foreach (var inputDto in inputDTOs)
            {
                var outPutDTO = new OutPutDTO();
                outPutDTO.CreditCharge = (5 * inputDto.AnnualPemium) / 100;
                outPutDTO.TotalPremium = inputDto.AnnualPemium + outPutDTO.CreditCharge;
                double dividentAverageAmount = outPutDTO.TotalPremium / 12;
                double monthlyAmount = Math.Round(dividentAverageAmount, 2);
                double monthlyAmountExcess = Math.Round(dividentAverageAmount, 10);
                double ExceedAmount = Math.Round((monthlyAmountExcess - monthlyAmount) * 12, 2);
                outPutDTO.InitialMonthlyPaymentAmount = monthlyAmount + ExceedAmount;
                outPutDTO.OtherMonthlyPaymentsAmount = monthlyAmount;
                outPutDTO.AnnualPemium = inputDto.AnnualPemium;
                outPutDTO.CustomerId = inputDto.CustomerId;
                outPutDTO.FirstName = inputDto.FirstName;
                outPutDTO.PayOutAmount = inputDto.PayOutAmount;
                outPutDTO.ProductName = inputDto.ProductName;
                outPutDTO.Surname = inputDto.Surname;
                outPutDTO.Title = inputDto.Title;
                outPutDTOs.Add(outPutDTO);
            }
            return outPutDTOs;
        }
    }
}
