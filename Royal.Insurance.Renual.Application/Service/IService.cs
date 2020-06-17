using Royal.Insurance.Renual.DTO;
using System.Collections.Generic;
namespace RoyalLondon.Insurance.Application.Service
{
    public interface IService
    {
        List<OutPutDTO> CustomerInsuranceGetAsync(InputData file);
    }
}
