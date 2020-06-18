using Royal.Insurance.Renual.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Royal.Insurance.Renual.Application.Service
{
    public static class MappingObject
    {
        public static OutPutDTO MapInputToOutPutObject(InputDTO inputDTO)
        {
            OutPutDTO outPutDTO = new OutPutDTO();
            outPutDTO.CustomerId = inputDTO.CustomerId;
            outPutDTO.Title = inputDTO.Title;
            outPutDTO.Surname = inputDTO.Surname;
            outPutDTO.FirstName = inputDTO.FirstName;
            outPutDTO.ProductName = inputDTO.ProductName;
            outPutDTO.PayOutAmount = inputDTO.PayOutAmount;
            outPutDTO.AnnualPemium = inputDTO.AnnualPemium; ;
            outPutDTO.CreditCharge = (5 * outPutDTO.AnnualPemium) / 100;
            outPutDTO.TotalPremium = outPutDTO.AnnualPemium + outPutDTO.CreditCharge;
            double divideAverageAmount = outPutDTO.TotalPremium / 12;
            double monthlyAmount = Math.Round(divideAverageAmount, 2);
            double monthlyAmountExcess = Math.Round(divideAverageAmount, 10);
            double exceedAmount = Math.Round((monthlyAmountExcess - monthlyAmount) * 12, 2);
            outPutDTO.InitialMonthlyPaymentAmount = monthlyAmount + exceedAmount;
            outPutDTO.OtherMonthlyPaymentsAmount = monthlyAmount;
           
            return outPutDTO;
        }
    }
}
