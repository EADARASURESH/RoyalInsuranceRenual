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
        public void Check_Ifile_Value()
        {
            var mockIserv = new Mock<IService>();
            var outPutDtos = new List<OutPutDTO>();
            var outputDto = new OutPutDTO();
            object obj = new object();
            var mockIServiceProvider = new Mock<IServiceProvider>();
            mockIServiceProvider.Setup(x => x.GetService(It.IsAny<Type>())).Returns(obj);
            var mockPremiumCalculation = new Mock<IPremiumCalculation>();
            mockPremiumCalculation.Setup(x => x.PremiumCalculationAmount(It.IsAny<InputDTO>())).Returns(outputDto);
            var mockMappingService= new Mock<IMappingSerrvice>();
            mockMappingService.Setup(x => x.MapService(It.IsAny<string>())).Returns(mockPremiumCalculation.Object);
            InputData inputData = new InputData();
            byte[] bytes = System.Convert.FromBase64String("SUQsVGl0bGUsRmlyc3ROYW1lLFN1cm5hbWUsUHJvZHVjdE5hbWUsUGF5b3V0QW1vdW50LEFubnVhbFByZW1pdW0NMSxNaXNzLFNhbGx5LFNtaXRoLFN0YW5kYXJkIENvdmVyLDE5MDgyMCwxMjMuNDUNMixNcixKb2huLFNtaXRoLEVuaGFuY2VkIENvdmVyLDgzMjA1LjUsMTIwDTMsTXJzLEhlbGVuLERhbmllbHMsU3BlY2lhbCBDb3ZlciwyMDAwMDAuOTksMTQxLjINCg==");
            inputData.CsvFile = bytes;
            mockIserv.Setup(x => x.CustomerInsuranceGetAsync(It.IsAny<InputData>())).Returns(outPutDtos);
            var mockCustomerInsuranceService = new CustomerInsuranceService(mockMappingService.Object);
            var outPut = mockCustomerInsuranceService.CustomerInsuranceGetAsync(inputData);
            Assert.AreEqual(3, outPut.Count);
        }

        [Test]
        public void Check_Ifile_Invalid_Value()
        {
            var mockIserv = new Mock<IService>();
            var outPutDtos = new List<OutPutDTO>();
            var outputDto = new OutPutDTO();
            object obj = new object();
            var mockIServiceProvider = new Mock<IServiceProvider>();
            mockIServiceProvider.Setup(x => x.GetService(It.IsAny<Type>())).Returns(obj);
            var mockPremiumCalculation = new Mock<IPremiumCalculation>();
            mockPremiumCalculation.Setup(x => x.PremiumCalculationAmount(It.IsAny<InputDTO>())).Returns(outputDto);
            var mockMappingService = new Mock<IMappingSerrvice>();
            mockMappingService.Setup(x => x.MapService(It.IsAny<string>())).Returns(mockPremiumCalculation.Object);
            InputData inputData = new InputData();
            byte[] bytes = null;
            inputData.CsvFile = bytes;
            mockIserv.Setup(x => x.CustomerInsuranceGetAsync(It.IsAny<InputData>())).Returns(outPutDtos);
            var mockCustomerInsuranceService = new CustomerInsuranceService(mockMappingService.Object);
            var outPut = mockCustomerInsuranceService.CustomerInsuranceGetAsync(inputData);
            Assert.AreEqual(3, outPut.Count);
        }

    }
}
