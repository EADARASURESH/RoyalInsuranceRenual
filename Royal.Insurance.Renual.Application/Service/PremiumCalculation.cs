using System;
using System.Collections.Generic;
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
        public IPremiumCalculation GetStreamService(int userSelection,List<InputDTO> _inputDtOs)
        {
            if (userSelection == 1)
            {
                return (IPremiumCalculation) _serviceProvider.GetService(typeof(PremiumCalculationByAnnualPremium));
            }
            else
            {
                return (IPremiumCalculation)_serviceProvider.GetService(typeof(PremiumCalCulationByProductType));
            }
           
        }
    }
}
