using Microsoft.Extensions.Configuration;
using Royal.Insurance.Renewal.DTO;

namespace Royal.Insurance.Renewal.Application.Service
{
    public class DefaultPremium : CommonImplimentation, IPremiumCalculation
    {
        public OutPutDTO PremiumCalculationAmount(InputDTO inputDtOs)
        {
            var outPutDto = new OutPutDTO();
            outPutDto = GetPremiumResult(inputDtOs);

            return outPutDto;
        }
        

    }
}
