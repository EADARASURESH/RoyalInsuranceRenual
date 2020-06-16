using Moq;
using NUnit.Framework;
using Royal.Insurance.Renual.Application.Controllers;
using Royal.Insurance.Renual.DTO;
using RoyalLondon.Insurance.Application.Service;
using System;
using System.Collections.Generic;

namespace Royal.Insura.Renual.Test
{
    public class InsuranceRenualTest
    {
        [Test]
        public void ByteLength_NULL_Negative_Check()
        {
            var mockIserv = new Mock<IService>();
            var outPutDto = new List<OutPutDTO>();
            OutPutDTO outPutDTO = new OutPutDTO();
            outPutDTO.AnnualPemium = 1.13;
            InputData inputData = new InputData();
            inputData.CsvFile = null;
            mockIserv.Setup(x => x.CustomerInsuranceGetAsync(It.IsAny<InputData>())).Returns(outPutDto);
            var inputService = new InsuranceRenualController(mockIserv.Object);
            var sample = inputService.RenualTextFiles(inputData);
            var statuscode = ((Microsoft.AspNetCore.Mvc.StatusCodeResult)sample).StatusCode;
            Assert.AreNotEqual(200, statuscode);
        }
        [Test]
        public void ByteLength_Positive_Negative_Check()
        {
            var mockIserv = new Mock<IService>();
            var outPutDto = new List<OutPutDTO>();
            OutPutDTO outPutDTO = new OutPutDTO();
            outPutDTO.AnnualPemium = 1.13;
            InputData inputData = new InputData();            
            mockIserv.Setup(x => x.CustomerInsuranceGetAsync(It.IsAny<InputData>())).Returns(outPutDto);
            var inputService = new InsuranceRenualController(mockIserv.Object);
            var sample = inputService.RenualTextFiles(inputData);
            var statuscode = ((Microsoft.AspNetCore.Mvc.StatusCodeResult)sample).StatusCode;
            Assert.AreNotEqual(200, statuscode);
        }
        [Test]
        public void ByteLength_Null_Check()
        {
            var mockIserv = new Mock<IService>();
            var outPutDto = new List<OutPutDTO>();
            InputData inputData = new InputData();
            inputData.CsvFile = null;
            mockIserv.Setup(x => x.CustomerInsuranceGetAsync(It.IsAny<InputData>())).Returns(outPutDto);
            var inputService = new InsuranceRenualController(mockIserv.Object);
            var sample = inputService.RenualTextFiles(inputData);
            var statuscode = ((Microsoft.AspNetCore.Mvc.StatusCodeResult)sample).StatusCode;
            Assert.AreNotEqual(200, statuscode);
        }
    }
}