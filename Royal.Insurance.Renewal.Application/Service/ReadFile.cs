using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Royal.Insurance.Renewal.Application.Service
{
    public static class ReadFile
    {
        public static async Task<string> GetFileName(IFormFile file)
        {
            Random random = new Random(10000);
            // full path to file in temp lo;cation
            //we are using Temp file name just for the example. Add your own file path.
            var filePath = Path.GetTempPath() + random.Next(0, 1000).ToString() + ".csv";
            await using (file.OpenReadStream())
            {
                if (file.Length > 0)
                {
                    await using var stream = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(stream);
                }
            }
            return filePath;
        }
    }
}
