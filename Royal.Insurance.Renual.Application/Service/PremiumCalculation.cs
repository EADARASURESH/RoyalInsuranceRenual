using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Royal.Insurance.Renual.DTO;

namespace Royal.Insurance.Renual.Application.Service
{
    public class PremiumCalculation 
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        public PremiumCalculation(IServiceProvider serviceProvider,IConfiguration configuration)
        {
            this._serviceProvider = serviceProvider;
            _configuration = configuration;
        }
        public IPremiumCalculation GetStreamService()
        {
            var getConfigValues = _configuration.GetValue<string>("ProductTypeDiscount:ProductsWiseDiscount");
            if (!string.IsNullOrEmpty(getConfigValues))
            {
                return (IPremiumCalculation)_serviceProvider.GetService(typeof(PremiumCalculationByProductType)); 
            }
            else
            {
                return (IPremiumCalculation)_serviceProvider.GetService(typeof(PremiumCalculationByAnnualPremium));
            }
        }
    }
}
