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
            if(file.CsvFile==null)
            {
                throw new ArgumentNullException();
            }
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
    }
}
