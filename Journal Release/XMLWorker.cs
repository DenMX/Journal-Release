using Journal_Release.Constructor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Journal_Release
{
    class XMLWorker
    {
        private static string Path = Environment.CurrentDirectory + @"\Settings.xml";
        static XDocument xdoc;

        private static readonly string settingsPath = $@"{Environment.CurrentDirectory}\psSettings";
        private static readonly string biosSettings = $@"{Environment.CurrentDirectory}\biosSettings";
        private static readonly string biosSetPass = $@"{Environment.CurrentDirectory}\biosSetPass";
        private static readonly string biosResetPass = $@"{Environment.CurrentDirectory}\biosResetPass";

        #region GETXML

        public static void GetXML()//Load XML
        {
            if(xdoc == null)
            {
                if (XMLExist())
                {
                    xdoc = XDocument.Load(Path);
                    CheckModels();
                }
                else
                    CreateXML();
                DataBase.CreateNewModel += AddModel;
            }
        }
        
        private static bool XMLExist()
        {
            if (File.Exists(Path))
                return true;
            else
                return false;
        }

        #region programs

        public static List<Soft> GetPrograms(string programType)
        {
            List<Soft> softList = new List<Soft>();
            XElement root = xdoc.Element("Settings").Element("programs");
            foreach (XElement program in root.Elements("program"))
            {
                if(program.Attribute("name").Value == programType)
                {
                    foreach(XElement xElem in program.Elements("program"))
                    {
                        Soft soft = new Soft(xElem.Attribute("name").Value, xElem.Element("path").Value, xElem.Element("parametr").Value);
                        softList.Add(soft);
                    }
                    return softList;
                }
            }
            return softList;
        }

        public static List<string> GetProgramTypes()
        {
            List<string> types = new List<string>();
            if (!XMLExist())
                return null;
            XDocument xDocument = XDocument.Load(Path);
            foreach(XElement xElement in xDocument.Element("Settings").Element("programs").Elements("program"))
            {
                types.Add(xElement.Attribute("name").Value);
            }

            return types;
        }
        #endregion
        #region drivers

        public static List<Driver> GetDrivers(string modelName)
        {
            if(xdoc == null)
            {
                xdoc = XDocument.Load(Path);
                CheckModels();
            }
            List<Driver> driverList = new List<Driver>();
            XElement root = xdoc.Element("Settings").Element("drivers");
            
            if(root.HasElements)
            {
                foreach (var xElem in root.Elements("driver"))
                {
                    if (xElem.Attribute("name").Value == modelName)
                    {
                        if (xElem.HasElements)
                        {
                            foreach (XElement program in xElem.Elements("driver"))
                            {
                                XAttribute nameAttribute = program.Attribute("name");
                                XElement path = program.Element("path");
                                XElement parametr = program.Element("parametr");
                                Driver driver = new Driver(nameAttribute.Value, path.Value, parametr.Value);
                                driverList.Add(driver);
                            }
                        }
                        
                        return driverList;
                    }
                }
            }
            return null;
            
        }
        #endregion
        #region settings
        public static string GetSettings()
        {
            if (File.Exists(settingsPath))
            {
                string settings;
                settings = File.ReadAllText(settingsPath);
                return settings;
            }
            return null;
        }
        #endregion
        #region biossettings
        public static string GetBiosScript(string model)
        {
            if (File.Exists(biosSettings + model))
            {
                string script = File.ReadAllText(biosSettings+model);
                return script;
            }
            return null;
        }

        public static string GetInstallingBiosPassScript(string model)
        {
            if (File.Exists(biosSetPass + model))
            {
                string setPass = File.ReadAllText(biosSetPass + model);
                return setPass;
            }
            return null;
        }

        public static string GetResetBiosPassScript(string model)
        {
            if (File.Exists(biosResetPass + model))
            {
                string reset = File.ReadAllText(biosResetPass + model);
                return reset;
            }
            return null;
        }
        #endregion

        private static void CreateXML()
        {
            XDocument xdoc = new XDocument();

            XElement programs = new XElement("programs");
            XElement drivers = new XElement("drivers");
            XElement Settings = new XElement("Settings");

            Settings.Add(programs);
            Settings.Add(drivers);
            xdoc.Add(Settings);

            xdoc.Save(Path);
        }

        #endregion

        #region UPDATEXML

        #region Soft

        public static void AddSoftType(string softName)
        {
            if(xdoc == null)
            {
                xdoc = XDocument.Load(Path);
            }
            XElement root = xdoc.Element("Settings").Element("programs");
            XElement softType = new XElement("program");
            XAttribute softTypeName = new XAttribute("name", softName);

            softType.Add(softTypeName);
            root.Add(softType);

            xdoc.Save(Path);
        }

        public static void DeleteSoft(string softType, string softName)
        {
            XElement root = xdoc.Element("Settings").Element("programs");
            foreach (XElement child in root.Elements("program"))
            {
                if (child.Attribute("name").Value == softType)
                {
                    foreach (XElement target in child.Elements("program"))
                    {
                        if (target.Attribute("name").Value == softName)
                        {
                            target.Remove();
                            xdoc.Save(Path);
                            return;
                        }
                    }
                }
            }
        }

        #endregion
        #region driver

        private static void CheckModels()
        {
            foreach(string model in DataBase.GetModelList())
            {
                bool isExist = false;
                foreach(XElement xModel in xdoc.Element("Settings").Element("drivers").Elements("driver"))
                {
                    if(xModel.Attribute("name").Value == model)
                        isExist = true;
                }
                if (!isExist)
                    AddModel(model);
            }
        }

        public static void AddModel(string model)
        {
            XElement root = xdoc.Element("Settings").Element("drivers");
            XElement driver = new XElement("driver");
            XAttribute driverName =  new XAttribute("name", model);
            
            driver.Add(driverName);
            root.Add(driver);

            xdoc.Save(Path);
        }

        public static void DeleteDriver(string model, string driverName)
        {

            XElement root = xdoc.Element("Settings").Element("drivers");
            foreach(XElement child in root.Elements("driver"))
            {
                if(child.Attribute("name").Value == (model))
                {
                    foreach(XElement target in child.Elements("driver"))
                    {
                        if(target.Attribute("name").Value == driverName)
                        {
                            target.Remove();
                            xdoc.Save(Path);
                            return;
                        }
                    }
                }
            }

           
        }
        #endregion
        #region settings
        public static void UpdateSettings(string settings)
        {
            File.WriteAllText(settingsPath, settings);
        }
        #endregion
        #region bios
        public static void BiosSettingsChange(string model, string script, string installingPassword, string resetPassword)
        {
            File.WriteAllText(biosSettings + model, script);
            File.WriteAllText(biosSetPass + model, installingPassword);
            File.WriteAllText(biosResetPass + model, resetPassword);
        }


        #endregion
        public static void AddXMLElement(string programsordiver, string softtypeormodel, string name, string path, string parametr)//Create for program's types or for models - XMLElement wich contains program or driver with parametrs
        {
            
            XElement root = xdoc.Element("Settings").Element(programsordiver);
            string value = "";//Receiving soft or driver to next work
            if(programsordiver == "programs")
            {
                value = "program";
            }
            else if(programsordiver == "drivers")
            {
                value = "driver";
            }
            foreach(var child in root.Elements(value))
            {
                if(child.Attribute("name").Value == (softtypeormodel))
                {
                    XElement program = new XElement(value);
                    XAttribute programName = new XAttribute("name", name);
                    XElement xPath = new XElement("path", path);
                    XElement xParam = new XElement("parametr", parametr);
                    
                    program.Add(programName);
                    program.Add(xPath);
                    program.Add(xParam);

                    child.Add(program);

                    xdoc.Save(Path);
                    return;
                }
            }
        }
        //UPDATE SOFTCLICK ADD
        //SCROLLVIEW FOR GRIDS ON SETTINGSWINDOW
        //INSTALLATION
        #endregion
    }
}
