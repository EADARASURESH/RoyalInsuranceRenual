using Microsoft.Extensions.Configuration;
using Royal.Insurance.Renual.DTO;
using System;
using System.Collections.Generic;

namespace Royal.Insurance.Renual.Application.Service
{
    public class DefaultPremium : IPremiumCalculation
    {
        private readonly IConfiguration _configuration;
        private readonly ICommonProductType _commonProductType;

        public DefaultPremium(ICommonProductType commonProductType, IConfiguration configuration)
        {
            _commonProductType = commonProductType;
            _configuration = configuration;
        }
        public OutPutDTO PremiumCalculationAmount(InputDTO inputDtOs)
        {
            var outPutDto = new OutPutDTO();
            outPutDto=_commonProductType.PremiumCalculationAmount(inputDtOs);
            return outPutDto;
        }
    }
}
