using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Royal.Insurance.Renual.DTO;
using RoyalLondon.Insurance.Application.Service;

namespace Royal.Insurance.Renual.Application.Service
{
    public class CustomerInsuranceService : IService
    {
        private readonly IPremiumCalculation _premiumCalculation;
        public CustomerInsuranceService(IPremiumCalculation premiumCalculation)
        {
            _premiumCalculation = premiumCalculation;
        }
        public List<OutPutDTO> CustomerInsuranceGetAsync(InputData file)
        {
            if (file.CsvFile == null)
            {
                throw new ArgumentNullException();
            }
            var stream = new MemoryStream(file.CsvFile);
            IFormFile files = new FormFile(stream, 0, 100000000, "name", "fileName");
            List<OutPutDTO> outPutDtOs;
            string filepath = ReadFile.GetFileName(files).Result;
            try
            {
                using var reader = new StreamReader(filepath, Encoding.Default);
                using var csvReader = new CsvReader(reader, CultureInfo.CurrentCulture);
                csvReader.Configuration.RegisterClassMap<MapObject>();
                var inputDtOs = csvReader.GetRecords<InputDTO>().ToList();
                outPutDtOs = _premiumCalculation.PremiumCalculationAmount(inputDtOs);
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
            return outPutDtOs;
        }
    }
}
