using Microsoft.AspNetCore.Mvc;
using Royal.Insurance.Renual.Application.Service;
using Royal.Insurance.Renual.DTO;
using System;
using System.Collections.Generic;
namespace Royal.Insurance.Renual.Application.Controllers
{
    [Route(Constant.ApiVersion)]
    [ApiController]
    public class InsuranceRenualController : ControllerBase
    {
        private readonly IService _renualService;
        public InsuranceRenualController(IService renualService)
        {
            _renualService = renualService;
        }
        [HttpPost]
        public IActionResult RenualTextFiles(InputData stream)
        {
            if (stream.CsvFile == null)
            {
                return BadRequest();
            }
            var outPutResult = new List<OutPutDTO>();
            try
            {
                outPutResult = _renualService.CustomerInsuranceGetAsync(stream);
            }
            catch (Exception ex)
            {
                Logger.InsertLogs(ex);
            }
            return Ok(outPutResult);
        }

    }
}
