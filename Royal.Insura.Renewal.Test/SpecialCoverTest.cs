using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Royal.Insurance.Renewal.Application.Service;
using Royal.Insurance.Renewal.DTO;

namespace Royal.Insurance.Renewal.Test
{
    public class SpecialCoverTest
    {
        
        Mock<IProductTypeInfo> productTypeInfo = new Mock<IProductTypeInfo>();
        OutPutDTO outPutDto = new OutPutDTO();

        public SpecialCoverTest()
        {
            productTypeInfo.Setup(x => x.GetProductTypeData());
           
        }

        [Test]
        public void Initial_Amount_Positive_calculation()
        {
            var inputDto = new InputDTO() { AnnualPemium = 120 };
            var defaultCalculator = new SpecialCover(productTypeInfo.Object);
            var result = defaultCalculator.PremiumCalculationAmount(inputDto);
            Assert.AreEqual(10.5, result.InitialMonthlyPaymentAmount);
        }

        [Test]
        public void Initial_Amount_Negative_calculation()
        {
            var inputDto = new InputDTO() { AnnualPemium = 120 };
            var defaultCalculator = new SpecialCover(productTypeInfo.Object);
            var result = defaultCalculator.PremiumCalculationAmount(inputDto);
            Assert.AreNotEqual(10, result.InitialMonthlyPaymentAmount);
        }

        [Test]
        public void OtherMonth_Amount_Positive_calculation()
        {
            var inputDto = new InputDTO() { AnnualPemium = 120 };
            var defaultCalculator = new SpecialCover(productTypeInfo.Object);
            var result = defaultCalculator.PremiumCalculationAmount(inputDto);
            Assert.AreEqual(10.5, result.OtherMonthlyPaymentsAmount);
        }

        [Test]
        public void OtherMonth_Amount_Negative_calculation()
        {
            var inputDto = new InputDTO() { AnnualPemium = 120 };
            var defaultCalculator = new SpecialCover(productTypeInfo.Object);
            var result = defaultCalculator.PremiumCalculationAmount(inputDto);
            Assert.AreNotEqual(10, result.OtherMonthlyPaymentsAmount);
        }

        [Test]
        public void OtherMonth_Amount_Empty_calculation()
        {
            var inputDto = new InputDTO() { AnnualPemium = 0 };
            var defaultCalculator = new SpecialCover(productTypeInfo.Object);
            var result = defaultCalculator.PremiumCalculationAmount(inputDto);
            Assert.AreNotEqual(10, result.OtherMonthlyPaymentsAmount);
        }

        [Test]
        public void InitialMonth_Amount_Empty_calculation()
        {
            var inputDto = new InputDTO() { AnnualPemium = 0 };
            var defaultCalculator = new SpecialCover(productTypeInfo.Object);
            var result = defaultCalculator.PremiumCalculationAmount(inputDto);
            Assert.AreNotEqual(10, result.InitialMonthlyPaymentAmount);
        }

        [Test]
        public void Title_Value_Check()
        {
            var inputDto = new InputDTO() { Title = "Mr" };
            var defaultCalculator = new SpecialCover(productTypeInfo.Object);
            var result = defaultCalculator.PremiumCalculationAmount(inputDto);
            Assert.AreEqual(inputDto.Title, result.Title);
        }

        [Test]
        public void FirstName_Value_Check()
        {
            var inputDto = new InputDTO() { FirstName = "Shalin" };
            var defaultCalculator = new SpecialCover(productTypeInfo.Object);
            var result = defaultCalculator.PremiumCalculationAmount(inputDto);
            Assert.AreEqual(inputDto.FirstName, result.FirstName);
        }

        [Test]
        public void ProductName_Value_Check()
        {
            var inputDto = new InputDTO() { ProductName = "Nestlay" };
            var defaultCalculator = new SpecialCover(productTypeInfo.Object);
            var result = defaultCalculator.PremiumCalculationAmount(inputDto);
            Assert.AreEqual(inputDto.ProductName, result.ProductName);
        }

        [Test]
        public void Surname_Amount_Empty_calculation()
        {
            var inputDto = new InputDTO() { Surname = "Mark" };
            var defaultCalculator = new SpecialCover(productTypeInfo.Object);
            var result = defaultCalculator.PremiumCalculationAmount(inputDto);
            Assert.AreEqual(inputDto.Surname, result.Surname);
        }

        [Test]
        public void PayOutAmount_Amount_Empty_calculation()
        {
            var inputDto = new InputDTO() { PayOutAmount = 0 };
            var defaultCalculator = new SpecialCover(productTypeInfo.Object);
            var result = defaultCalculator.PremiumCalculationAmount(inputDto);
            Assert.AreNotEqual(10, result.PayOutAmount);
        }
    }
}
