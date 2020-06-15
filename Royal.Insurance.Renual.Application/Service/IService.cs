using Microsoft.AspNetCore.Http;
using Royal.Insurance.Renual.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RoyalLondon.Insurance.Application.Service
{
    public interface IService
    {
        List<OutPutDTO> CustomerInsuranceGetAsync(InputData file);
        MultipartContent GetStream(InputData file);
    }
}
