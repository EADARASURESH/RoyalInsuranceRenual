using Royal.Insurance.Renewal.DTO;

namespace Royal.Insurance.Renewal.Application.Service
{
    public interface IPremiumCalculation
    {
        OutPutDTO PremiumCalculationAmount(InputDTO inputDtOs);
    }
}
