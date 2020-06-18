using Royal.Insurance.Renual.DTO;
using System;

namespace Royal.Insurance.Renual.Application.Service
{
    public class CommonProductType : ICommonProductType
    {
        public OutPutDTO PremiumCalculationAmount(InputDTO inputDto)
        {
            var outPutDto = new OutPutDTO();
            try
            {
                outPutDto= MappingObject.MapInputToOutPutObject(inputDto);
            }
            catch (Exception exception)
            {
                Logger.InsertLogs(exception);
            }

            return outPutDto;
        }
    }
}
