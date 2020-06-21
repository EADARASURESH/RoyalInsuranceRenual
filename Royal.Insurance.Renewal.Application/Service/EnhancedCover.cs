using Royal.Insurance.Renewal.DTO;
using System.Collections.Generic;

namespace Royal.Insurance.Renewal.Application.Service
{
    public class EnhancedCover : CommonImplimentation, IPremiumCalculation
    {
        private readonly IProductTypeInfo _productTypeInfo;
        public readonly List<ProductTypeDiscount> _productTypeDiscounts;

        public EnhancedCover(IProductTypeInfo productTypeInfo)
        {
            _productTypeInfo = productTypeInfo;
            _productTypeDiscounts = _productTypeInfo.GetProductTypeData();
        }

        public override OutPutDTO GetPremiumResult(InputDTO inputDtos)
        {
            var outPutDto = new OutPutDTO();
            var getDiscount = GetConfigProductInfo(_productTypeDiscounts, inputDtos.ProductName);
            if (inputDtos.PayOutAmount > 15000000)
            {
                if (getDiscount > 0)
                {
                    var anualPremium = (getDiscount * inputDtos.AnnualPemium) / 100;
                    inputDtos.AnnualPemium = inputDtos.AnnualPemium - anualPremium;
                }
                else
                {
                    var anualPremium = (15 * inputDtos.AnnualPemium) / 100;
                    inputDtos.AnnualPemium = inputDtos.AnnualPemium - anualPremium;
                }
            }
            outPutDto = MapObject(inputDtos);

            return outPutDto;
        }

        public OutPutDTO PremiumCalculationAmount(InputDTO inputDto)
        {
            if (inputDto.PayOutAmount >= 15000000)
            {
                throw new System.Exception("Premium value should not exceed 15000000 " + inputDto.CustomerId);
            }
               
            var outPutDto = new OutPutDTO();
            outPutDto = GetPremiumResult(inputDto);

            return outPutDto;
        }
    }
}
