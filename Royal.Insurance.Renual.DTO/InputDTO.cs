﻿using CsvHelper.Configuration.Attributes;namespace Royal.Insurance.Renual.DTO{    public class InputDTO    {        [Name("ID")]        public long CustomerId { get; set; }        [Name("Title")]        public string Title { get; set; }        [Name("FirstName")]        public string FirstName { get; set; }        [Name("Surname")]        public string Surname { get; set; }        [Name("ProductName")]        public string ProductName { get; set; }        [Name("PayoutAmount")]        public double PayOutAmount { get; set; }        [Name("AnnualPremium")]        public double AnnualPemium { get; set; }    }}