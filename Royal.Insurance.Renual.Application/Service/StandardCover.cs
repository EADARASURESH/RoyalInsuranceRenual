using Microsoft.Extensions.Configuration;
using Royal.Insurance.Renual.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Royal.Insurance.Renual.Application.Service
{
    public class StandardCover : IPremiumCalculation
    {
        private readonly IProductTypeInfo _productTypeInfo;

        private readonly IConfiguration _configuration;
        private readonly ICommonProductType _premiumCalculation;
        public StandardCover(IProductTypeInfo productTypeInfo, IConfiguration configuration, ICommonProductType premiumCalculation)
        {
            _productTypeInfo = productTypeInfo;
            _configuration = configuration;
            _premiumCalculation = premiumCalculation;
        }
        public OutPutDTO PremiumCalculationAmount(InputDTO inputDtOs)
        {
            var outPutDto = new OutPutDTO();
            var result = _productTypeInfo.GetProductTypeData();
            var getDiscount = result != null ? result.Where(x => x.ProductName == inputDtOs.ProductName).Select(y => y.Discount).FirstOrDefault() : 0;
            if (getDiscount > 0)
            {
                var anualPremium = (getDiscount * inputDtOs.AnnualPemium) / 100;
                inputDtOs.AnnualPemium = inputDtOs.AnnualPemium - anualPremium;
            }
            else
            {
                var anualPremium = (20 * inputDtOs.AnnualPemium) / 100;
                inputDtOs.AnnualPemium = inputDtOs.AnnualPemium - anualPremium;
            }
            outPutDto = _premiumCalculation.PremiumCalculationAmount(inputDtOs);
            return outPutDto;
        }
    }
}
