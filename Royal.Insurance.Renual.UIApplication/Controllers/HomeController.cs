using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Royal.Insurance.Renual.DTO;
using Royal.Insurance.Renual.UIApplication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
namespace Royal.Insurance.Renual.UIApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
#pragma warning disable 618
        private readonly IHostingEnvironment _hostingEnvironment;
#pragma warning restore 618
      
#pragma warning disable 618
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment hostingEnvironment)
#pragma warning restore 618
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
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
                    var stringContent = new StringContent(myContent, Encoding.UTF8, "application/json");
                    var result = httpClient.PostAsync("https://localhost:44330/api/InsuranceRenual", stringContent).Result;
                    objectResponce = result.Content.ReadAsAsync<List<OutPutDTO>>().Result;
                    foreach (var outdto in objectResponce)
                    {
                        outdto.FileName = outdto.CustomerId + "_" + outdto.FirstName;
                        outdto.TextFile = Convert.ToBase64String(System.IO.File.ReadAllBytes(CreateText.GetStream(outdto, _hostingEnvironment)));
                    }
                }
            }
            return Json(objectResponce);
        }
    }
}
