using Royal.Insurance.Renual.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Royal.Insurance.Renual.Application.Service
{
    public class PremiumCalculationByProductType : IPremiumCalculation
    {
        private readonly IProductTypeInfo _productTypeInfo;

        private readonly IConfiguration _configuration;
        public PremiumCalculationByProductType(IProductTypeInfo productTypeInfo, IConfiguration configuration)
        {
            _productTypeInfo = productTypeInfo;
            _configuration = configuration;
        }

        public List<OutPutDTO> PremiumCalculationAmount(List<InputDTO> inputDtOs)
        {
            var outPutDtOs = new List<OutPutDTO>();
            try
            {
                var productDiscountInfo = GetProductTypeData();
                foreach (var inputDto in inputDtOs)
                {
                    var outPutDto = new OutPutDTO();
                    
                    if (productDiscountInfo != null)
                    {
                        var productHasDiscount = productDiscountInfo.Where(x => x.ProductName == inputDto.ProductName)
                            .Select(y => y.Discount);
                        var annualPremiumDiscount = (productHasDiscount.FirstOrDefault() * inputDto.AnnualPemium) / 100;
                        inputDto.AnnualPemium = inputDto.AnnualPemium - annualPremiumDiscount;
                    }
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
            }
            catch (Exception exception)
            {
                Logger.InsertLogs(exception);
            }

            return outPutDtOs;
        }

        public List<ProductTypeDiscount> GetProductTypeData( )
        {
            List<ProductTypeDiscount> productTypeDiscounts=null;
            var getString = _configuration.GetValue<string>("ProductTypeDiscount:ProductsWiseDiscount");
            if (!string.IsNullOrEmpty(getString))
            {
                productTypeDiscounts = _productTypeInfo.GetProductTypeData(getString);
            }
            return productTypeDiscounts;
        }
    }
}
