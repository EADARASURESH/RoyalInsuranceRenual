namespace Royal.Insurance.Renewal.Application.Service
{
    public interface IMappingSerrvice
    {
        IPremiumCalculation MapService(string productName);
    }
}