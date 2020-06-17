using CsvHelper.Configuration;
using Royal.Insurance.Renual.DTO;
namespace Royal.Insurance.Renual.Application.Service
{
    public sealed class MapObject : ClassMap<InputDTO>
    {
        public MapObject()
        {
            Map(m => m.CustomerId).Name("ID");
            Map(m => m.AnnualPemium).Name("AnnualPremium");
            Map(m => m.FirstName).Name("FirstName");
            Map(m => m.Title).Name("Title");
            Map(m => m.PayOutAmount).Name("PayoutAmount");
            Map(m => m.ProductName).Name("ProductName");
            Map(m => m.Surname).Name("Surname");
        }
    }
}
