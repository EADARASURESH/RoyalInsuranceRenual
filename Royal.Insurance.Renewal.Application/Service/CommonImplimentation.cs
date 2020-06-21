using Royal.Insurance.Renewal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Royal.Insurance.Renewal.Application.Service
{
    public abstract class CommonImplimentation
    {
        public virtual OutPutDTO PremiumCalculationAmount(InputDTO inputDto)
        {
            var outPutDto = new OutPutDTO();
            try
            {
                outPutDto = MapObject(inputDto);
            }
            catch (Exception exception)
            {
                Logger.InsertLogs(exception);
            }

            return outPutDto;
        }

        public double GetConfigProductInfo(List<ProductTypeDiscount> productTypeInfo,string productName)
        {
            var getDiscount = productTypeInfo != null ? productTypeInfo.Where(x => x.ProductName == productName).Select(y => y.Discount).FirstOrDefault() : 0;
            
            return getDiscount;
        }

        public OutPutDTO MapObject(InputDTO inputDTO)
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
