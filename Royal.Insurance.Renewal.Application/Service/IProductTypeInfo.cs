using System.Collections.Generic;

namespace Royal.Insurance.Renewal.Application.Service
{
    public interface IProductTypeInfo
    {
        List<ProductTypeDiscount> GetProductTypeData();
    }
}
