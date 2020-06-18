using CsvHelper.Configuration;
using Royal.Insurance.Renual.DTO;
namespace Royal.Insurance.Renual.Application.Service
{
    public sealed class MapObject : ClassMap<InputDTO>
    {
        public MapObject()
        {
            Map(m => m.CustomerId).Name(Constant.Id);
            Map(m => m.AnnualPemium).Name(Constant.AnnualPremium);
            Map(m => m.FirstName).Name(Constant.FileName);
            Map(m => m.Title).Name(Constant.Title);
            Map(m => m.PayOutAmount).Name(Constant.PayoutAmount);
            Map(m => m.ProductName).Name(Constant.ProductName);
            Map(m => m.Surname).Name(Constant.Surname);
        }
    }
}
