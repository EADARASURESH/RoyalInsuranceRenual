using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Royal.Insurance.Renual.Application.Service;
using Royal.Insurance.Renual.DTO;

namespace Royal.Insurance.Renual.Test
{
    public class PremiumCalculationTes
    {

        [Test]
        public void Initial_Amount_Positive_calculation()
        {
            var mockReviser = new Mock<IPremiumCalculation>();
            var inputDto = new List<InputDTO>() { new InputDTO() { AnnualPemium = 120 } };
            var outputDto = new List<OutPutDTO>();
            mockReviser.Setup(x => x.PremiumCalculationAmount(It.IsAny<List<InputDTO>>())).Returns(outputDto);
            var premiumCalculationService = new PremiumCalculationService();
            var result = premiumCalculationService.PremiumCalculationAmount(inputDto);
            Assert.AreEqual(10.5, result[0].InitialMonthlyPaymentAmount);
        }
        [Test]
        public void Initial_Amount_Negative_calculation()
        {
            var mockReviser = new Mock<IPremiumCalculation>();
            var inputDto = new List<InputDTO>() { new InputDTO() { AnnualPemium = 120 } };
            var outputDto = new List<OutPutDTO>();
            mockReviser.Setup(x => x.PremiumCalculationAmount(It.IsAny<List<InputDTO>>())).Returns(outputDto);
            var premiumCalculationService = new PremiumCalculationService();
            var result = premiumCalculationService.PremiumCalculationAmount(inputDto);
            Assert.AreNotEqual(10, result[0].InitialMonthlyPaymentAmount);
        }
        [Test]
        public void OtherMonth_Amount_Positive_calculation()
        {
            var mockReviser = new Mock<IPremiumCalculation>();
            var inputDto = new List<InputDTO>() { new InputDTO() { AnnualPemium = 120 } };
            var outputDto = new List<OutPutDTO>();
            mockReviser.Setup(x => x.PremiumCalculationAmount(It.IsAny<List<InputDTO>>())).Returns(outputDto);
            var premiumCalculationService = new PremiumCalculationService();
            var result = premiumCalculationService.PremiumCalculationAmount(inputDto);
            Assert.AreNotEqual(10.5, result[0].OtherMonthlyPaymentsAmount);
        }
        [Test]
        public void OtherMonth_Amount_Negative_calculation()
        {
            var mockReviser = new Mock<IPremiumCalculation>();
            var inputDto = new List<InputDTO>() { new InputDTO() { AnnualPemium = 120 } };
            var outputDto = new List<OutPutDTO>();
            mockReviser.Setup(x => x.PremiumCalculationAmount(It.IsAny<List<InputDTO>>())).Returns(outputDto);
            var premiumCalculationService = new PremiumCalculationService();
            var result = premiumCalculationService.PremiumCalculationAmount(inputDto);
            Assert.AreNotEqual(10, result[0].InitialMonthlyPaymentAmount);
        }
        [Test]
        public void OtherMonth_Amount_Empty_calculation()
        {
            var mockReviser = new Mock<IPremiumCalculation>();
            var inputDto = new List<InputDTO>() { new InputDTO() { AnnualPemium = 0 } };
            var outputDto = new List<OutPutDTO>();
            mockReviser.Setup(x => x.PremiumCalculationAmount(It.IsAny<List<InputDTO>>())).Returns(outputDto);
            var premiumCalculationService = new PremiumCalculationService();
            var result = premiumCalculationService.PremiumCalculationAmount(inputDto);
            Assert.AreNotEqual(10, result[0].InitialMonthlyPaymentAmount);
        }
        [Test]
        public void Title_Value_Check()
        {
            var mockReviser = new Mock<IPremiumCalculation>();
            var inputDto = new List<InputDTO>() { new InputDTO() { Title = "Mr" } };
            var outputDto = new List<OutPutDTO>();
            mockReviser.Setup(x => x.PremiumCalculationAmount(It.IsAny<List<InputDTO>>())).Returns(outputDto);
            var premiumCalculationService = new PremiumCalculationService();
            var result = premiumCalculationService.PremiumCalculationAmount(inputDto);
            Assert.AreEqual(inputDto[0].Title, result[0].Title);
        }
        [Test]
        public void FirstName_Value_Check()
        {
            var mockReviser = new Mock<IPremiumCalculation>();
            var inputDto = new List<InputDTO>() { new InputDTO() { FirstName = "Shalin" } };
            var outputDto = new List<OutPutDTO>();
            mockReviser.Setup(x => x.PremiumCalculationAmount(It.IsAny<List<InputDTO>>())).Returns(outputDto);
            var premiumCalculationService = new PremiumCalculationService();
            var result = premiumCalculationService.PremiumCalculationAmount(inputDto);
            Assert.AreEqual(inputDto[0].FirstName, result[0].FirstName);
        }
        [Test]
        public void ProductName_Value_Check()
        {
            var mockReviser = new Mock<IPremiumCalculation>();
            var inputDto = new List<InputDTO>() { new InputDTO() { ProductName = "Nestlay" } };
            var outputDto = new List<OutPutDTO>();
            mockReviser.Setup(x => x.PremiumCalculationAmount(It.IsAny<List<InputDTO>>())).Returns(outputDto);
            var premiumCalculationService = new PremiumCalculationService();
            var result = premiumCalculationService.PremiumCalculationAmount(inputDto);
            Assert.AreEqual(inputDto[0].ProductName, result[0].ProductName);
        }
        [Test]
        public void Surname_Amount_Empty_calculation()
        {
            var mockReviser = new Mock<IPremiumCalculation>();
            var inputDto = new List<InputDTO>() { new InputDTO() { Surname = "Mark" } };
            var outputDto = new List<OutPutDTO>();
            mockReviser.Setup(x => x.PremiumCalculationAmount(It.IsAny<List<InputDTO>>())).Returns(outputDto);
            var premiumCalculationService = new PremiumCalculationService();
            var result = premiumCalculationService.PremiumCalculationAmount(inputDto);
            Assert.AreEqual(inputDto[0].Surname, result[0].Surname);
        }
        [Test]
        public void PayOutAmount_Amount_Empty_calculation()
        {
            var mockIserv = new Mock<IPremiumCalculation>();
            var inputDto = new List<InputDTO>() { new InputDTO() { PayOutAmount = 10.3 } };
            var outputDto = new List<OutPutDTO>();
            mockIserv.Setup(x => x.PremiumCalculationAmount(It.IsAny<List<InputDTO>>())).Returns(outputDto);
            var premiumCalculationService = new PremiumCalculationService();
            var result = premiumCalculationService.PremiumCalculationAmount(inputDto);
            Assert.AreEqual(inputDto[0].PayOutAmount, result[0].PayOutAmount);
        }
    }
}
