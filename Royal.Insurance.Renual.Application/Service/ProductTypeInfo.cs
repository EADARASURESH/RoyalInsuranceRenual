using System;
using System.Collections.Generic;

namespace Royal.Insurance.Renual.Application.Service
{
    public class ProductTypeInfo : IProductTypeInfo
    {
        public List<ProductTypeDiscount> GetProductTypeData(string inputString)
        {
            var productTypeDiscount = new List<ProductTypeDiscount>();
            try
            {
                string[] productInfo = inputString.Split('|');
                for (int i = 0; i < productInfo.Length; i++)
                {
                    string[] product = productInfo[i].Split(':');
                    ProductTypeDiscount productObj = new ProductTypeDiscount();
                    productObj.ProductName = product[0];
                    productObj.Discount = Convert.ToDouble(product[1]);
                    productTypeDiscount.Add(productObj);
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
