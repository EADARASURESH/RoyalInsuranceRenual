using Royal.Insurance.Renewal.DTO;

namespace Royal.Insurance.Renewal.Application.Service
{
    public interface ICommonProductType
    {
        OutPutDTO PremiumCalculationAmount(InputDTO inputDto);
    }
}
