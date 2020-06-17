﻿using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Royal.Insurance.Renual.Application.Service;
using Royal.Insurance.Renual.DTO;
using RoyalLondon.Insurance.Application.Service;

namespace Royal.Insurance.Renual.Test
{
    public class CustomerInsuranceServiceTest
    {
        [Test]
        public void Check_IFILE_VALUE()
        {
            var mockIserv = new Mock<IService>();
            var outPutDto = new List<OutPutDTO>();
            var mockPremiumCalculation = new Mock<IPremiumCalculation>();
            mockPremiumCalculation.Setup(x => x.PremiumCalculationAmount(It.IsAny<List<InputDTO>>())).Returns(outPutDto);
            InputData inputData = new InputData();
            byte[] bytes = System.Convert.FromBase64String("SUQsVGl0bGUsRmlyc3ROYW1lLFN1cm5hbWUsUHJvZHVjdE5hbWUsUGF5b3V0QW1vdW50LEFubnVhbFByZW1pdW0NMSxNaXNzLFNhbGx5LFNtaXRoLFN0YW5kYXJkIENvdmVyLDE5MDgyMCwxMjMuNDUNMixNcixKb2huLFNtaXRoLEVuaGFuY2VkIENvdmVyLDgzMjA1LjUsMTIwDTMsTXJzLEhlbGVuLERhbmllbHMsU3BlY2lhbCBDb3ZlciwyMDAwMDAuOTksMTQxLjINCg==");
            inputData.CsvFile = bytes;

            mockIserv.Setup(x => x.CustomerInsuranceGetAsync(It.IsAny<InputData>())).Returns(outPutDto);
            var mockCustomerInsuranceService = new CustomerInsuranceService(mockPremiumCalculation.Object);
            var outPut = mockCustomerInsuranceService.CustomerInsuranceGetAsync(inputData);
            Assert.AreEqual(0, outPut.Count);
        }

    }
}