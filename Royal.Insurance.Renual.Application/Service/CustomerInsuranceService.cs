using CsvHelper;
using Microsoft.AspNetCore.Http;
using Royal.Insurance.Renual.Application.Service;
using Royal.Insurance.Renual.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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
            string filepath = ReadFile.GetFileName(files).Result;
            try
            {
                using (var reader = new StreamReader(filepath, Encoding.Default))
                using (var csvReader = new CsvReader(reader, CultureInfo.CurrentCulture))
                {
                    csvReader.Configuration.RegisterClassMap<MapObject>();
                    inputDTOs = csvReader.GetRecords<InputDTO>().ToList();
                    PremiumCalculation premiumCalculation = new PremiumCalculation();
                    outPutDTOs = premiumCalculation.PremiumCalculationAmount(inputDTOs);
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
    }
}
