using System;
using System.Collections.Generic;
using System.Text;

namespace Journal_Release.Constructor
{
    class Soft
    {
        public string SoftName { get; set; }

        public string SoftPath { get; set; }

        public string SoftParametr { get; set; }

        public Soft(string name, string path, string param)
        {
            SoftName = name;
            SoftPath = path;
            SoftParametr = param;
        }



    }
}
