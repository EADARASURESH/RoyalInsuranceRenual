using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Royal.Insurance.Renual.Application.Service
{
    public sealed class Logger
    {
        private static Logger instance = null;
        public static Logger GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new Logger();
                return instance;
            }
        }
        private Logger()
        {
           
        }
        public static void InsertLogs(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
