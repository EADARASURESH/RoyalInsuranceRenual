using Microsoft.Extensions.Configuration;
using Royal.Insurance.Renual.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Royal.Insurance.Renual.Application.Service
{
    public class SpecialCover : IPremiumCalculation
    {
        private readonly IConfiguration _configuration;
        private readonly ICommonProductType _commonProductType;
        private readonly IProductTypeInfo _productTypeInfo;
        public SpecialCover(ICommonProductType commonProductType, IConfiguration configuration, IProductTypeInfo productTypeInfo)
        {
            _commonProductType = commonProductType;
            _configuration = configuration;
            _productTypeInfo = productTypeInfo;
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
                var anualPremium = (10 * inputDtOs.AnnualPemium) / 100;
                inputDtOs.AnnualPemium = inputDtOs.AnnualPemium - anualPremium;
            }
            outPutDto = _commonProductType.PremiumCalculationAmount(inputDtOs);
            return outPutDto;
        }
    }
}
