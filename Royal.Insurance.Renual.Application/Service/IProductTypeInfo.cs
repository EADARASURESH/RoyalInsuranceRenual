using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Royal.Insurance.Renual.Application.Service
{
    public interface IProductTypeInfo
    {
        List<ProductTypeDiscount> GetProductTypeData(string inputString);
    }
}
