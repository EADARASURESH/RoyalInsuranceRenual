using System.Collections.Generic;
using Royal.Insurance.Renual.DTO;

namespace Royal.Insurance.Renual.Application.Service
{
    public interface IService
    {
        List<OutPutDTO> CustomerInsuranceGetAsync(InputData file);
    }
}
