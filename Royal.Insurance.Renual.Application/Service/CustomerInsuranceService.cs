using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Royal.Insurance.Renual.DTO;

namespace Royal.Insurance.Renual.Application.Service
{
    public class CustomerInsuranceService : IService
    {
        private readonly PremiumCalculation _premiumCalculation;
        public CustomerInsuranceService(PremiumCalculation premiumCalculation, IConfiguration configuration, IProductTypeInfo productTypeInfo)
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
            IFormFile files = new FormFile(stream, 0, Constant.Size, Constant.Name, Constant.Filename);
            var outPutDtOs=new List<OutPutDTO>();
            string filepath = ReadFile.GetFileName(files).Result;
            try
            {
                using var reader = new StreamReader(filepath, Encoding.Default);
                using var csvReader = new CsvReader(reader, CultureInfo.CurrentCulture);
                csvReader.Configuration.RegisterClassMap<MapObject>();
                var inputDtOs = csvReader.GetRecords<InputDTO>().ToList();
                foreach (var inPutDto in inputDtOs)
                {
                    OutPutDTO outPutDtO = _premiumCalculation.MapService(inPutDto).PremiumCalculationAmount(inPutDto);
                    outPutDtOs.Add(outPutDtO);
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
            return outPutDtOs;
        }
    }
}
