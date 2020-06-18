using Royal.Insurance.Renual.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Royal.Insurance.Renual.Application.Service
{
    public interface ICommonProductType
    {
        OutPutDTO PremiumCalculationAmount(InputDTO inputDto);
    }
}
