﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Royal.Insurance.Renual.DTO;
using Royal.Insurance.Renual.UIApplication.Models;

namespace Royal.Insurance.Renual.UIApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<ActionResult> Index(IFormFile files)
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
                    inputData.Name = "csvfiledata";
                    var myContent = JsonConvert.SerializeObject(inputData);
                    var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json"); 
                    var result = httpClient.PostAsync("https://localhost:44330/api/InsuranceRenual", stringContent).Result;
                    objectResponce = result.Content.ReadAsAsync<List<OutPutDTO>>(new[] { new JsonMediaTypeFormatter() }).Result;
                   
                }

            }

           
            return View(objectResponce);
        }
        [HttpPost]
        public StringBuilder GetStream(OutPutDTO outPutDto)
        {
            var obj = outPutDto.ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append(DateTime.Now.Date.ToString());
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(outPutDto.Title + " " + outPutDto.FirstName);
            sb.Append("RE: Your Renewal");
            sb.Append(Environment.NewLine);
            sb.Append("Dear customer’s " + outPutDto.Title + " " + outPutDto.FirstName + " " + outPutDto.Surname);
            sb.Append(Environment.NewLine);
            sb.Append("We hereby invite you to renew your Insurance Policy, subject to the following terms.");
            sb.Append(Environment.NewLine);
            sb.Append("Your chosen insurance product is " + outPutDto.ProductName + ".");
            sb.Append(Environment.NewLine);
            sb.Append("The amount payable to you in the event of a valid claim will be £" + outPutDto.PayOutAmount + ".");
            sb.Append(Environment.NewLine);
            sb.Append("Your annual premium will be £" + outPutDto.AnnualPemium + ".");
            sb.Append(Environment.NewLine);
            sb.Append("If you choose to pay by Direct Debit, we will add a credit charge of £" + outPutDto.CreditCharge + ", bringing the total to £" + outPutDto.TotalPremium + ".");
            sb.Append(Environment.NewLine);
            sb.Append("This is payable by an initial payment of £" + outPutDto.InitialMonthlyPaymentAmount + ", followed by 11 payments of £" + outPutDto.OtherMonthlyPaymentsAmount + " each.");
            sb.Append(Environment.NewLine);
            sb.Append("Please get in touch with us to arrange your renewal by visiting https://www.regallutoncodingtest.co.uk/renew or calling us on 01625 123456.");
            sb.Append(Environment.NewLine);
            sb.Append("Kind Regards");
            sb.Append(Environment.NewLine);
            sb.Append("Regal Luton");
            string myTempFile = Path.Combine(Path.GetTempPath(), outPutDto.CustomerId + "_" + outPutDto.FirstName + ".txt");
            using (StreamWriter sw = new StreamWriter(myTempFile))
            {
                sw.WriteLine(sb);
            }
            return sb;
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
                    inputData.Name = "csvfiledata";
                    var myContent = JsonConvert.SerializeObject(inputData);
                    var stringContent = new StringContent(myContent, UnicodeEncoding.UTF8, "application/json");
                    var result = httpClient.PostAsync("https://localhost:44330/api/InsuranceRenual", stringContent).Result;
                    objectResponce = result.Content.ReadAsAsync<List<OutPutDTO>>().Result;
                    foreach (var outdto in objectResponce)
                    {
                        outdto.TextFile = GetStream(outdto).ToString();
                    }
                }

            }

            return Json(objectResponce);
        }
        public IActionResult Privacy(string stringvalue)
        {

            return View();
        }
        [HttpPost]
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
