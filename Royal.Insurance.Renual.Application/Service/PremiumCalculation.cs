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
        public PremiumCalculation(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }
        public IPremiumCalculation MapService(InputDTO inputDto)
        {
            string caseType = inputDto.ProductName;
            switch (caseType)
            {
                case "Standard Cover":
                    return (IPremiumCalculation)_serviceProvider.GetService(typeof(StandardCover));
                case "Enhanced Cover":
                    return (IPremiumCalculation)_serviceProvider.GetService(typeof(EnhancedCover));
                case "Special Cover":
                    return (IPremiumCalculation)_serviceProvider.GetService(typeof(SpecialCover));
                default:
                    return (IPremiumCalculation)_serviceProvider.GetService(typeof(DefaultPremium));
            }
            
        }
    }
}
