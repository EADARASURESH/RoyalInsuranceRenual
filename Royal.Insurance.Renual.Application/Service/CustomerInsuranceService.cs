
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Recognizers.Text;
using Newtonsoft.Json;
using Royal.Insurance.Renual.Application.Service;
using Royal.Insurance.Renual.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RoyalLondon.Insurance.Application.Service
{
    public class CustomerInsuranceService : IService
    {
        public List<OutPutDTO> CustomerInsuranceGetAsync(InputData file)
        {
            var stream = new MemoryStream(file.CsvFile);
            IFormFile files = new FormFile(stream, 0, 100000000, "name", "fileName");
            List<InputDTO> inputDTOs = null;
            List<OutPutDTO> outPutDTOs = null;
            ReadFile readFile = new ReadFile();
            string filepath = readFile.GetFileName(files).Result;
            try
            {
                using (var reader = new StreamReader(filepath, Encoding.Default))
                using (var csvReader = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    csvReader.Configuration.RegisterClassMap<MapObject>();
                    inputDTOs = csvReader.GetRecords<InputDTO>().ToList();
                    outPutDTOs = GetOutPut(inputDTOs);
                }
            }
            catch (UnauthorizedAccessException e)
            {
                throw new Exception(e.Message);
            }
            catch (FieldValidationException e)
            {
                throw new Exception(e.Message);
            }
            catch (CsvHelperException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return outPutDTOs;
        }
        public List<OutPutDTO> GetOutPut(List<InputDTO> inputDTOs)
        {
            var outPutDTOs = new List<OutPutDTO>() ;


            foreach (var inputDto in inputDTOs)
            {
                var outPutDTO = new OutPutDTO();
                outPutDTO.CreditCharge = (5 * inputDto.AnnualPemium)/ 100;
                outPutDTO.TotalPremium = inputDto.AnnualPemium + outPutDTO.CreditCharge;
                double dividentAverageAmount = outPutDTO.TotalPremium / 12;
                double monthlyAmount= Math.Round(dividentAverageAmount, 2);
                double monthlyAmountExcess = Math.Round(dividentAverageAmount, 10);
                double ExceedAmount = Math.Round((monthlyAmountExcess - monthlyAmount) * 12,2);
                outPutDTO.InitialMonthlyPaymentAmount = monthlyAmount + ExceedAmount;
                outPutDTO.OtherMonthlyPaymentsAmount = monthlyAmount;
                outPutDTO.AnnualPemium = inputDto.AnnualPemium;
                outPutDTO.CustomerId = inputDto.CustomerId;
                outPutDTO.FirstName = inputDto.FirstName;
                outPutDTO.PayOutAmount = inputDto.PayOutAmount;
                outPutDTO.ProductName = inputDto.ProductName;
                outPutDTO.Surname = inputDto.Surname;
                outPutDTO.Title = inputDto.Title;
                outPutDTOs.Add(outPutDTO);
            }
            return outPutDTOs;
        }

        public MultipartContent GetStream(InputData file)
        {
            var outObject = CustomerInsuranceGetAsync(file);
            var content = new MultipartContent();
            foreach (var outPutDto in outObject)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(DateTime.Now.Date.ToString());
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(outPutDto.Title +" "+ outPutDto.FirstName);
                sb.Append("RE: Your Renewal");
                sb.Append(Environment.NewLine);
                sb.Append("Dear customer’s " + outPutDto.Title + " " + outPutDto.FirstName + " " + outPutDto.Surname);
                sb.Append(Environment.NewLine);
                sb.Append("We hereby invite you to renew your Insurance Policy, subject to the following terms.");
                sb.Append(Environment.NewLine);
                sb.Append("Your chosen insurance product is "+ outPutDto.ProductName+".");
                sb.Append(Environment.NewLine);
                sb.Append("The amount payable to you in the event of a valid claim will be £"+outPutDto.PayOutAmount+".");
                sb.Append(Environment.NewLine);
                sb.Append("Your annual premium will be £"+ outPutDto.AnnualPemium+ ".");
                sb.Append(Environment.NewLine);
                sb.Append("If you choose to pay by Direct Debit, we will add a credit charge of £"+outPutDto.CreditCharge+", bringing the total to £"+outPutDto.TotalPremium+".");
                sb.Append(Environment.NewLine);
                sb.Append("This is payable by an initial payment of £"+outPutDto.InitialMonthlyPaymentAmount+ ", followed by 11 payments of £"+outPutDto.OtherMonthlyPaymentsAmount+" each.");
                sb.Append(Environment.NewLine);
                sb.Append("Please get in touch with us to arrange your renewal by visiting https://www.regallutoncodingtest.co.uk/renew or calling us on 01625 123456.");
                sb.Append(Environment.NewLine);
                sb.Append("Kind Regards");
                sb.Append(Environment.NewLine);
                sb.Append("Regal Luton");
                string myTempFile = Path.Combine(Path.GetTempPath(), outPutDto.CustomerId+"_"+outPutDto.FirstName+".txt");
                using (StreamWriter sw = new StreamWriter(myTempFile))
                {
                    sw.WriteLine(sb);
                }
                var filestremContect = new StreamContent(new FileStream(myTempFile, FileMode.Open));
                filestremContect.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("image/jpeg");
                content.Add(filestremContect);
            }
            return content;
        }
    }
}
