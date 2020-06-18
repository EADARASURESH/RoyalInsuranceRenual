using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Royal.Insurance.Renual.DTO;
using Royal.Insurance.Renual.UIApplication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace Royal.Insurance.Renual.UIApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGetText _iGetTextService;

        private readonly IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IGetText iGetTextService, IConfiguration configuration)
        {
            _logger = logger;
            _iGetTextService = iGetTextService;
            _configuration = configuration;
        }
        public ActionResult Index(IFormFile files)
        {
            return View();
        }
        public JsonResult GetJsonResult(IFormFile files)
        {
            var objectResponce = new List<OutPutDTO>();
            if (files != null)
            {
                byte[] byteArray;
                using (BinaryReader br = new BinaryReader(files.OpenReadStream()))
                {
                    byteArray = br.ReadBytes((int)files.OpenReadStream().Length);
                    // Convert the image in to bytes
                }
                InputData inputData = new InputData();
                inputData.CsvFile = byteArray;
                using (var httpClient = new HttpClient())
                {
                    var myContent = JsonConvert.SerializeObject(inputData);
                    var stringContent = new StringContent(myContent, Encoding.UTF8, Constant.ContentType);
                    var result = httpClient.PostAsync(_configuration.GetValue<string>(Constant.BaseUrl), stringContent).Result;
                    objectResponce = result.Content.ReadAsAsync<List<OutPutDTO>>().Result;
                    foreach (var outdto in objectResponce)
                    {
                        outdto.FileName = outdto.CustomerId + "_" + outdto.FirstName;
                        outdto.TextFile = Convert.ToBase64String(System.IO.File.ReadAllBytes(_iGetTextService.GetStream(outdto)));
                    }
                }
            }
            return Json(objectResponce);
        }
    }
}
