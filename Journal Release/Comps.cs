using System.Collections.Generic;

namespace Journal_Release
{
    class Comps
    {
        public string Id { get; set; }
        public string PcName { get; set; }
        public string Responsibility { get; set; }
        public string Date { get; set; }
        public string AdmPass { get; set; }
        public string BiosPass { get; set; }
        public string Model { get; set; }

        public static List<Comps> compsList = new List<Comps>();

        public delegate void AddComp(Comps comp);
        public static event AddComp NewComp = delegate { };
        
        private static Comps lastComp;
        public static Comps LastComp
        {
            get { return lastComp; }
            set
            {
                lastComp = value;
                compsList.Add(value);
                NewComp(lastComp);
            }
        }

        public Comps()
        {

        }

        public Comps(string id, string pcName, string date, string resosibility, string admPass, string biosPass, string model)
        {
            Id = id;
            PcName = pcName;
            Responsibility = resosibility;
            Date = date;
            AdmPass = admPass;
            BiosPass = biosPass;
            Model = model;
            LastComp = this;
        }

        public static bool isNew(string pcName)
        {
            foreach(Comps comp in compsList)
            {
                if(ClearWhiteSpace(comp.PcName) == pcName.ToUpper())
                {
                    return false;
                }
            }
            return true;
        }

        public static Comps GetPasswords(string pcName)
        {
            foreach(Comps comp in compsList)
            {
                if(ClearWhiteSpace(comp.PcName) == pcName.ToUpper())
                {
                    return comp;
                }
            }
            return null;
        }

        private static string ClearWhiteSpace(string pcName)
        {
            string result = "";
            for(int i=0; i<pcName.Length; i++)
            {
                if (!char.IsWhiteSpace(pcName[i]))
                {
                    result += pcName[i];
                }
            }
            return result;
        }

        internal static string GetId(string pcName)
        {
            foreach(Comps comp in compsList)
            {
                if (comp.PcName == pcName.ToUpper())
                    return comp.Id;
            }
            return null;
        }
    }
}