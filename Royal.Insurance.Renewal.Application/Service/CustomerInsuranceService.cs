using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Royal.Insurance.Renewal.DTO;

namespace Royal.Insurance.Renewal.Application.Service
{
    public class CustomerInsuranceService : IService
    {
        private readonly IMappingSerrvice _mappingService;

        public CustomerInsuranceService(IMappingSerrvice mappingService)
        {
            _mappingService = mappingService;
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
                var inputDtos = csvReader.GetRecords<InputDTO>().ToList();
                foreach (var inPutDto in inputDtos)
                {
                    OutPutDTO outPutDtO = _mappingService.MapService(inPutDto.ProductName).PremiumCalculationAmount(inPutDto);
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
