using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Royal.Insurance.Renual.Application.Service
{
    public class ProductTypeInfo : IProductTypeInfo
    {
        private readonly IConfiguration _configuration;

        public ProductTypeInfo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<ProductTypeDiscount> GetProductTypeData()
        {
            string inputString = _configuration.GetValue<string>("ProductTypeDiscount: ProductsWiseDiscount");
            var productTypeDiscount = new List<ProductTypeDiscount>();
            try
            {
                if (!string.IsNullOrEmpty(inputString))
                {
                    string[] productInfo = inputString.Split('|');
                    if (!string.IsNullOrEmpty(inputString))
                    {
                        for (int i = 0; i < productInfo.Length; i++)
                        {
                            string[] product = productInfo[i].Split(':');
                            ProductTypeDiscount productObj = new ProductTypeDiscount();
                            productObj.ProductName = product[0];
                            productObj.Discount = Convert.ToDouble(product[1]);
                            productTypeDiscount.Add(productObj);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.InsertLogs(ex);
            }
           
            return productTypeDiscount;
        }
       
    }
}
