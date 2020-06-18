using Microsoft.AspNetCore.Hosting;
using Royal.Insurance.Renual.DTO;
using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Royal.Insurance.Renual.UIApplication.Models
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
                var objpath = _hostingEnvironment.ContentRootPath + "\\CustomerText.txt";
                string text = File.ReadAllText(objpath);
                StringBuilder sb = new StringBuilder();
                sb.Append(text);
                sb.Replace("$CurrentDate", DateTime.Now.Date.ToString("dd/MM/yyyy"));
                sb.Replace("$FULLNAME", outPutDto.Title + " " + outPutDto.FirstName);
                sb.Replace("$WITHSURNAME", outPutDto.Title + " " + outPutDto.FirstName + " " + outPutDto.Surname);
                sb.Replace("$PRODUCTNAME", outPutDto.ProductName);
                sb.Replace("$PATOUTAMOUNT", "£" + outPutDto.PayOutAmount.ToString(CultureInfo.InvariantCulture));
                sb.Replace("$ANNUALPREMIUM", "£" + outPutDto.AnnualPemium.ToString(CultureInfo.InvariantCulture));
                sb.Replace("$CREDITCHARGE", "£" + outPutDto.CreditCharge.ToString(CultureInfo.InvariantCulture));
                sb.Replace("$TOTALPREMIUM", "£" + outPutDto.TotalPremium.ToString(CultureInfo.InvariantCulture));
                sb.Replace("$INITIALMONTHPREMIUM", "£" + outPutDto.InitialMonthlyPaymentAmount.ToString(CultureInfo.InvariantCulture));
                sb.Replace("$OTHERMONTHPREMIUM", "£" + outPutDto.OtherMonthlyPaymentsAmount.ToString(CultureInfo.InvariantCulture));
                myTempFilePath = Path.Combine(Path.GetTempPath(), outPutDto.CustomerId + "_" + outPutDto.FirstName + ".txt");
                using StreamWriter sw = new StreamWriter(myTempFilePath);
                sw.WriteLine(sb);
               
            }
            return myTempFilePath;
        }
    }
}
