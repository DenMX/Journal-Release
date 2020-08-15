using System;
using System.Collections.Generic;
using System.Text;

namespace Journal_Release.Constructor
{
    class Driver
    {
        public string DriverName { get; set;  }
        public string DriverPath { get; set; }
        public string DriverParametr { get; set; }

        public  Driver(string driverName, string driverPath, string driverParametr)
        {
            DriverName = driverName;
            DriverPath = driverPath;
            DriverParametr = driverParametr;
        }
    }
}
