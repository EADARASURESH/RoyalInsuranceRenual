using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Royal.Insurance.Renewal.Application.Service;
using Royal.Insurance.Renewal.DTO;
using Royal.Insurance.Renual.Application.Controllers;

namespace Royal.Insurance.Renewal.Test
{
    public class InsuranceRenewalTest
    {
        [Test]
        public void ByteLength_NULL_Negative_Check()
        {
            var mockIserv = new Mock<IService>();
            var outPutDots = new List<OutPutDTO>();
            OutPutDTO outPutDto = new OutPutDTO { AnnualPemium = 1.13 };
            InputData inputData = new InputData { CsvFile = null };
            mockIserv.Setup(x => x.CustomerInsuranceGetAsync(It.IsAny<InputData>())).Returns(outPutDots);
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
            OutPutDTO outPutDTO = new OutPutDTO { AnnualPemium = 1.13 };
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