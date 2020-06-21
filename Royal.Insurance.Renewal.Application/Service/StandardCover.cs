using Royal.Insurance.Renewal.DTO;
using System.Collections.Generic;

namespace Royal.Insurance.Renewal.Application.Service
{
    public class StandardCover : CommonImplimentation, IPremiumCalculation
    {
        private readonly IProductTypeInfo _productTypeInfo;
        public readonly List<ProductTypeDiscount> _productTypeDiscounts;
        public StandardCover(IProductTypeInfo productTypeInfo)
        {
            _productTypeInfo = productTypeInfo;
            _productTypeDiscounts = _productTypeInfo.GetProductTypeData();

        }

        public override OutPutDTO PremiumCalculationAmount(InputDTO inputDtOs)
        {
            var outPutDto = new OutPutDTO();
            var getDiscount = GetConfigProductInfo(_productTypeDiscounts, inputDtOs.ProductName);
            if (inputDtOs.PayOutAmount > 65000)
            {
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
            }
            outPutDto = MapObject(inputDtOs);

            return outPutDto;
        }
    }
}
