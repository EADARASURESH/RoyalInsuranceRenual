using Royal.Insurance.Renewal.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Royal.Insurance.Renewal.Application.Service
{
    public abstract class CommonImplimentation
    {
        public virtual OutPutDTO GetPremiumResult(InputDTO inputDto)
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

        public OutPutDTO MapObject(InputDTO inputDto)
        {
            OutPutDTO outPutDto = new OutPutDTO();
            outPutDto.CustomerId = inputDto.CustomerId;
            outPutDto.Title = inputDto.Title;
            outPutDto.Surname = inputDto.Surname;
            outPutDto.FirstName = inputDto.FirstName;
            outPutDto.ProductName = inputDto.ProductName;
            outPutDto.PayOutAmount = inputDto.PayOutAmount;
            outPutDto.AnnualPemium = inputDto.AnnualPemium; ;
            outPutDto.CreditCharge = (5 * outPutDto.AnnualPemium) / 100;
            outPutDto.TotalPremium = outPutDto.AnnualPemium + outPutDto.CreditCharge;
            double divideAverageAmount = outPutDto.TotalPremium / 12;
            double monthlyAmount = Math.Round(divideAverageAmount, 2);
            double monthlyAmountExcess = Math.Round(divideAverageAmount, 10);
            double exceedAmount = Math.Round((monthlyAmountExcess - monthlyAmount) * 12, 2);
            outPutDto.InitialMonthlyPaymentAmount = monthlyAmount + exceedAmount;
            outPutDto.OtherMonthlyPaymentsAmount = monthlyAmount;

            return outPutDto;
        }

    }
}
