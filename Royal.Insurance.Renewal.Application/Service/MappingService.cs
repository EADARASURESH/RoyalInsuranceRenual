using System;

namespace Royal.Insurance.Renewal.Application.Service
{
    public class MappingService:IMappingSerrvice
    {
        private readonly IServiceProvider _serviceProvider;

        public MappingService(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public IPremiumCalculation MapService(string productName)
        {
            string caseType = productName;
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
