using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Royal.Insurance.Renual.DTO;
using RoyalLondon.Insurance.Application.Service;

namespace Royal.Insurance.Renual.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceRenualController : ControllerBase
    {
        private readonly IService _renualService;
        public InsuranceRenualController(IService renualService)
        {
            _renualService = renualService;
        }
        [HttpPost]
        public  List<OutPutDTO> RenualTextFiles(InputData stream)
        {
            var outPutResult = new List<OutPutDTO>();
            try
            {
                outPutResult =  _renualService.CustomerInsuranceGetAsync(stream);
                
            }
            catch (Exception ex)
            {

            }
            return outPutResult;
        }
        [HttpPost]
        [Route("GenerateFile")]
        public HttpResponseMessage Get(InputData inputData)
        {
            var content = _renualService.GetStream(inputData);            
            var ids = new List<int>() { 1, 2 };
            var objectContent = new ObjectContent<List<int>>(ids, new System.Net.Http.Formatting.JsonMediaTypeFormatter());
            content.Add(objectContent);
            var response = new HttpResponseMessage();
            response.Content = content;
            return response;
        }
       
    }
}
