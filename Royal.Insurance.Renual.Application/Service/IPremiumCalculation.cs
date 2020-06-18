using Royal.Insurance.Renual.DTO;
using System.Collections.Generic;

namespace Royal.Insurance.Renual.Application.Service
{
    public interface IPremiumCalculation
    {
        OutPutDTO PremiumCalculationAmount(InputDTO inputDtOs);
    }
}
