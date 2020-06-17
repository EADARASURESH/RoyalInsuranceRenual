using NUnit.Framework;
using Royal.Insurance.Renual.Application.Service;
using Royal.Insurance.Renual.DTO;
using System;
using System.Collections.Generic;
using System.Text;
namespace Royal.Insura.Renual.Test
{
    public class PremiumCalculationTes
    {
        [Test]
        public void Initial_Amount_Positive_calculation()
        {
            var inputDTO = new List<InputDTO>() { new InputDTO() { AnnualPemium = 120 } };
            var input =new PremiumCalculation();
            var result=input.PremiumCalculationAmount(inputDTO);
            Assert.AreEqual(10.5, result[0].InitialMonthlyPaymentAmount);
        }
        [Test]
        public void Initial_Amount_Negative_calculation()
        {
            var inputDTO = new List<InputDTO>() { new InputDTO() { AnnualPemium = 120 } };
            var input = new PremiumCalculation();
            var result = input.PremiumCalculationAmount(inputDTO);
            Assert.AreNotEqual(10, result[0].InitialMonthlyPaymentAmount);
        }
        [Test]
        public void OtherMonth_Amount_Positive_calculation()
        {
            var inputDTO = new List<InputDTO>() { new InputDTO() { AnnualPemium = 120 } };
            var input = new PremiumCalculation();
            var result = input.PremiumCalculationAmount(inputDTO);
            Assert.AreEqual(10.5, result[0].OtherMonthlyPaymentsAmount);
        }
        [Test]
        public void OtherMonth_Amount_Negative_calculation()
        {
            var inputDTO = new List<InputDTO>() { new InputDTO() { AnnualPemium = 120 } };
            var input = new PremiumCalculation();
            var result = input.PremiumCalculationAmount(inputDTO);
            Assert.AreNotEqual(10, result[0].InitialMonthlyPaymentAmount);
        }
        [Test]
        public void OtherMonth_Amount_Empty_calculation()
        {
            var inputDTO = new List<InputDTO>() { new InputDTO() { AnnualPemium = 0 } };
            var input = new PremiumCalculation();
            var result = input.PremiumCalculationAmount(inputDTO);
            Assert.AreNotEqual(10.5, result[0].InitialMonthlyPaymentAmount);
        }
        [Test]
        public void Title_Value_Check()
        {
            var inputDTO = new List<InputDTO>() { new InputDTO() { Title = "Mr" } };
            var input = new PremiumCalculation();
            var result = input.PremiumCalculationAmount(inputDTO);
            Assert.AreEqual(inputDTO[0].Title, result[0].Title);
        }
        [Test]
        public void FirstName_Value_Check()
        {
            var inputDTO = new List<InputDTO>() { new InputDTO() { FirstName = "Shalin" } };
            var input = new PremiumCalculation();
            var result = input.PremiumCalculationAmount(inputDTO);
            Assert.AreEqual(inputDTO[0].FirstName, result[0].FirstName);
        }
        [Test]
        public void ProductName_Value_Check()
        {
            var inputDTO = new List<InputDTO>() { new InputDTO() { ProductName = "Nestlay" } };
            var input = new PremiumCalculation();
            var result = input.PremiumCalculationAmount(inputDTO);
            Assert.AreEqual(inputDTO[0].ProductName, result[0].ProductName);
        }
        [Test]
        public void Surname_Amount_Empty_calculation()
        {
            var inputDTO = new List<InputDTO>() { new InputDTO() { Surname = "Mark" } };
            var input = new PremiumCalculation();
            var result = input.PremiumCalculationAmount(inputDTO);
            Assert.AreEqual(inputDTO[0].Surname, result[0].Surname);
        }
        [Test]
        public void PayOutAmount_Amount_Empty_calculation()
        {
            var inputDTO = new List<InputDTO>() { new InputDTO() { PayOutAmount = 10.3 } };
            var input = new PremiumCalculation();
            var result = input.PremiumCalculationAmount(inputDTO);
            Assert.AreEqual(inputDTO[0].PayOutAmount, result[0].PayOutAmount);
        }
    }
}
