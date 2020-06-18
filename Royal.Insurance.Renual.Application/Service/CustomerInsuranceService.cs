using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Royal.Insurance.Renual.DTO;

namespace Royal.Insurance.Renual.Application.Service
{
    public class CustomerInsuranceService : IService
    {
        private readonly PremiumCalculation _premiumCalculation;
        public CustomerInsuranceService(PremiumCalculation premiumCalculation)
        {
            _premiumCalculation = premiumCalculation;
        }
        public List<OutPutDTO> CustomerInsuranceGetAsync(InputData file,int userSelection)
        {
            if (file.CsvFile == null)
            {
                throw new ArgumentNullException();
            }
            var stream = new MemoryStream(file.CsvFile);
            IFormFile files = new FormFile(stream, 0, Constant.Size, Constant.Name, Constant.Filename);
            List<OutPutDTO> outPutDtOs;
            string filepath = ReadFile.GetFileName(files).Result;
            try
            {
                using var reader = new StreamReader(filepath, Encoding.Default);
                using var csvReader = new CsvReader(reader, CultureInfo.CurrentCulture);
                csvReader.Configuration.RegisterClassMap<MapObject>();
                var inputDtOs = csvReader.GetRecords<InputDTO>().ToList();
                outPutDtOs = _premiumCalculation.GetStreamService(userSelection, inputDtOs).PremiumCalculationAmount(inputDtOs);
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
