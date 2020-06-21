using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Royal.Insurance.Renewal.Application.Service;
using Royal.Insurance.Renewal.DTO;

namespace Royal.Insurance.Renewal.Test
{
    public class CustomerInsuranceServiceTest
    {
        [Test]
        public void Check_IFILE_VALUE()
        {
            var mockIserv = new Mock<IService>();
            var outPutDtos = new List<OutPutDTO>();
            var outputDto = new OutPutDTO();
            var mockIServiceProvider = new Mock<IServiceProvider>();
            var mockPremiumService= new Mock<MappingService>(mockIServiceProvider.Object);
            var mockPremiumCalculation = new Mock<IPremiumCalculation>();
            mockPremiumCalculation.Setup(x => x.PremiumCalculationAmount(It.IsAny<InputDTO>())).Returns(outputDto);
            InputData inputData = new InputData();
            byte[] bytes = System.Convert.FromBase64String("SUQsVGl0bGUsRmlyc3ROYW1lLFN1cm5hbWUsUHJvZHVjdE5hbWUsUGF5b3V0QW1vdW50LEFubnVhbFByZW1pdW0NMSxNaXNzLFNhbGx5LFNtaXRoLFN0YW5kYXJkIENvdmVyLDE5MDgyMCwxMjMuNDUNMixNcixKb2huLFNtaXRoLEVuaGFuY2VkIENvdmVyLDgzMjA1LjUsMTIwDTMsTXJzLEhlbGVuLERhbmllbHMsU3BlY2lhbCBDb3ZlciwyMDAwMDAuOTksMTQxLjINCg==");
            inputData.CsvFile = bytes;

            mockIserv.Setup(x => x.CustomerInsuranceGetAsync(It.IsAny<InputData>())).Returns(outPutDtos);
            //var mockCustomerInsuranceService = new CustomerInsuranceService(mockPremiumService.Object);
            //var outPut = mockCustomerInsuranceService.CustomerInsuranceGetAsync(inputData);
            //Assert.AreEqual(0, outPut.Count);
        }

    }
}
