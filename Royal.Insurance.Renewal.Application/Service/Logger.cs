using System;

namespace Royal.Insurance.Renewal.Application.Service
{
    public sealed class Logger
    {
        private static Logger _instance;

        public static Logger GetInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new Logger();
                return _instance;
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
