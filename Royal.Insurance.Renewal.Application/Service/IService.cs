using System.Collections.Generic;
using Royal.Insurance.Renewal.DTO;

namespace Royal.Insurance.Renewal.Application.Service
{
    public interface IService
    {
        List<OutPutDTO> CustomerInsuranceGetAsync(InputData file);
    }
}
