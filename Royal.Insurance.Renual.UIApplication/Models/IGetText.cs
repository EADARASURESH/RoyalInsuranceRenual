using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Royal.Insurance.Renual.DTO;

namespace Royal.Insurance.Renual.UIApplication.Models
{
    public interface IGetText
    {
        string GetStream(OutPutDTO outPutDto);
    }
}
