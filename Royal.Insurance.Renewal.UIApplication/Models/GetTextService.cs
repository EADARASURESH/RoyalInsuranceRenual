using Microsoft.AspNetCore.Hosting;
using Royal.Insurance.Renewal.DTO;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Royal.Insurance.Renewal.UIApplication.Models
{
    public class GetTextService : IGetText
    {
        [Obsolete]
#pragma warning disable 618
        private readonly IHostingEnvironment _hostingEnvironment;
#pragma warning restore 618

        [Obsolete]
        public GetTextService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [Obsolete]
        public string GetStream(OutPutDTO outPutDto)
        {
            string myTempFilePath = string.Empty;
            if (_hostingEnvironment != null)
            {
                var objpath = _hostingEnvironment.ContentRootPath + Constant.FileTypeName;
                string text = File.ReadAllText(objpath);
                StringBuilder sb = new StringBuilder();
                sb.Append(text);
                sb.Replace(Constant.CurrentDate, DateTime.Now.Date.ToString(Constant.DateFormat))
                .Replace(Constant.FullName, outPutDto.Title + " " + outPutDto.FirstName)
                .Replace(Constant.WitthSurName, outPutDto.Title + " " + outPutDto.FirstName + " " + outPutDto.Surname)
                .Replace(Constant.PoductName, outPutDto.ProductName)
                .Replace(Constant.PayOutAmount, Constant.Pound + outPutDto.PayOutAmount.ToString(CultureInfo.InvariantCulture))
                .Replace(Constant.AnnualPremiumCharge, Constant.Pound + Math.Round(outPutDto.AnnualPemium, 2).ToString())
                .Replace(Constant.CreditCharge, Constant.Pound + Math.Round(outPutDto.CreditCharge, 2).ToString())
                .Replace(Constant.TotalPremium, Constant.Pound + Math.Round(outPutDto.TotalPremium, 2).ToString())
                .Replace(Constant.InitilaMonthPremium, Constant.Pound + outPutDto.InitialMonthlyPaymentAmount.ToString(CultureInfo.InvariantCulture))
                .Replace(Constant.OtherMonthPremium, Constant.Pound + outPutDto.OtherMonthlyPaymentsAmount.ToString(CultureInfo.InvariantCulture));
                myTempFilePath = Path.Combine(Path.GetTempPath(), outPutDto.CustomerId + "_" + outPutDto.FirstName + Constant.Extention);
                using StreamWriter sw = new StreamWriter(myTempFilePath);
                sw.WriteLine(sb);
               
            }
            return myTempFilePath;
        }
    }
}
