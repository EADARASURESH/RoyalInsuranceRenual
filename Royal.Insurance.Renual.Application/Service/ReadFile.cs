using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace RoyalLondon.Insurance.Application.Service
{
    public class ReadFile
    {
        public async Task<string> GetFileName(IFormFile file)
        {
            Random random = new Random(10000);
            // full path to file in temp lo;cation
            //we are using Temp file name just for the example. Add your own file path.
            var filePath = Path.GetTempPath() + random.Next(0,1000).ToString() + ".csv";
            using (var fileStram = file.OpenReadStream())
            {
                if (file.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }
            return filePath;
        }
    }
}
