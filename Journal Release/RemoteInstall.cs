using Journal_Release.Constructor;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Journal_Release
{
    class RemoteInstall
    {
        private string password = "";
        Password pass;

        private string admPass = "";
        private string biosPass = "";

        private string quote = "\"";
        

        public delegate void Progress(string status, int percent);
        public event Progress ProgressChange = delegate { };

        private int percent;

        private List<string> allPath;

        private int Percent 
        { 
            get 
            {
                return percent;
            }
            set
            {
                if(percent < 100)
                {
                    if (percent + value < 100)
                        percent += value;
                    else
                    {
                        percent = 100;
                        InstallFinished(this, EventArgs.Empty);
                        MessageBox.Show("Установка завершена!");
                    }

                    switch (percent)
                    {
                        case 33:
                            ProgressChange("Установка драйверов...", percent);
                            break;
                        case 66:
                            ProgressChange("Установка ПО...", percent);
                            break;
                        case 99:
                            ProgressChange("Последние настройки...", percent);
                            break;
                        case 100:
                            ProgressChange("Установка завершена!", percent);
                            break;
                        default:
                            ProgressChange("Идет подкотовка...", percent);
                            break;
                    }
                }
            } 
        }


        public EventHandler InstallFinished;


        public RemoteInstall(string aPass, string bPass)
        {
            allPath = new List<string>();
            admPass = aPass;
            biosPass = bPass;
        }

        public void Install(string pcName, string softType, string modelName)
        {
            if (Settings2.Default.scriptFolder == "")
            {
                MessageBox.Show("Необходимо указать папку для создания временных скриптов.");
                return;
            }
            //check for psExec extention
            if (Settings2.Default.psExec_Path == "")
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "EXE(*.exe) |*.exe";
                open.Title = "Укажите размещение psExec";
                open.FilterIndex = 2;
                if (open.ShowDialog() == true)
                {
                    Settings2.Default.psExec_Path = open.FileName;
                    Settings2.Default.Save();
                }
            }
            if(password.Length < 1)
            {
                    pass = new Password();
                    if (pass.ShowDialog() == true)
                    {
                        password = pass.Pass;
                    }               
            }

            XMLWorker.GetXML();
            Task.Run(() => InstallDrivers(pcName, modelName));
            Task.Run(() => InstallSoft(pcName, softType));
            
            if (Comps.isNew(pcName))
            {
                if (CreateSettings(pcName, FindPasswords(XMLWorker.GetSettings())))
                    Task.Run(() => SetSettings(pcName));
                if (XMLWorker.GetBiosScript(modelName) != "" && XMLWorker.GetBiosScript(modelName) != null)
                {
                    CreateBiosSettings(pcName, FindPasswords(XMLWorker.GetBiosScript(modelName)));
                    Task.Run(() => ResetBiosPassword(pcName, modelName));
                    Task.Run(() => BiosSettingsInstalling(pcName));
                    Task.Run(() => Thread.Sleep(120000));
                    Task.Run(() => SetBiosPassword(pcName, modelName));
                }
            }
            else
            {
                if (CreateSettings(pcName, FindPasswords(XMLWorker.GetSettings())))
                    Task.Run(() => SetSettings(pcName));
                Finish(this, EventArgs.Empty);
            }
             
        }

        #region Drivers
        private void InstallDrivers(string pcName, string modelName)
        {
            if(modelName != "")
            {
                if (XMLWorker.GetDrivers(modelName).Count != 0)
                {
                    try
                    {
                        
                        string localPath = $@"{Environment.CurrentDirectory}\{pcName}_drivers.bat";
                        allPath.Add(localPath);
                        if (File.Exists(localPath))
                            File.Delete(localPath);
                        foreach(Driver driver in XMLWorker.GetDrivers(modelName))
                        {
                            File.AppendAllText(localPath, $@"{Settings2.Default.psExec_Path} \\{pcName} -h -u {Environment.UserDomainName}\{Environment.UserName} -p {password} cmd /c {quote}{driver.DriverPath} {driver.DriverParametr}{quote}{Environment.NewLine}");
                        }
                        
                        using (Process process = new Process())
                        {
                            process.StartInfo.FileName = localPath;
                            //process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                            //process.StartInfo.CreateNoWindow = true;
                            process.EnableRaisingEvents = true;
                            process.Exited += ProgressUpdate;
                            process.Start();
                        }
                            
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show($@"Случилась непредвиденная ошибка: {e.Message}");
                        File.AppendAllText($@"{Environment.CurrentDirectory}\Logs", $@"{DateTime.Now}:{Environment.NewLine}{e.Source}{Environment.NewLine}{e.Message}");
                    }
                    
                }
            }
            
        }

        private void ProgressUpdate(object sender, EventArgs e)
        {
            Percent = 33;
        }

        private void GetDrivers(string modelName)
        {
            string tempDriver;
            string path = $@"{Settings2.Default.scriptFolder}\{modelName}.bat";

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            foreach (Driver driver in XMLWorker.GetDrivers(modelName))
            {
                tempDriver = driver.DriverPath + " " + driver.DriverParametr;
                File.AppendAllText(path, tempDriver + "\r\n");
            }


        }
        #endregion

        #region soft
        private void InstallSoft(string pcName, string softType)
        {
            if(softType != "")
            {
                if(XMLWorker.GetPrograms(softType).Count != 0)
                {
                    try
                    {
                        string localPath = $@"{Environment.CurrentDirectory}\{pcName}_soft.bat";
                        allPath.Add(localPath);

                        if (File.Exists(localPath))
                            File.Delete(localPath);
                        foreach(Soft soft in XMLWorker.GetPrograms(softType))
                        {
                            File.AppendAllText(localPath, $@"{Settings2.Default.psExec_Path} \\{pcName} -h -u {Environment.UserDomainName}\{Environment.UserName} -p {password} cmd /c {quote}{soft.SoftPath} {soft.SoftParametr}{quote}{Environment.NewLine}");
                        }
                        

                        using (Process process = new Process())
                        {
                            process.StartInfo.FileName = localPath;
                            process.Start();
                        }
                            
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show($@"Случилась непредвиденная ошибка: {e.Message}");
                        File.AppendAllText($@"{Environment.CurrentDirectory}\Logs", $@"{DateTime.Now}:\r\n{e.Source}\r\n{e.Message}");
                    }
                }
            }
        }

        private void GetSoft(string softType)
        {
            string path = $@"{Settings2.Default.scriptFolder}\{softType}.bat";
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            foreach(Soft soft in XMLWorker.GetPrograms(softType))
            {
                File.AppendAllText(path, $@"{soft.SoftPath} {soft.SoftParametr}{Environment.NewLine}");
            }
            
        }
        #endregion

        #region Settings

        private void SetSettings(string pcName)
        {
            try
            {
                string localPath = $@"{Environment.CurrentDirectory}\{pcName}_settings.bat";
                allPath.Add(localPath);
                string settings = $@"{Settings2.Default.psExec_Path} \\{pcName} -h -u {Environment.UserDomainName}\{Environment.UserName} -p {password} Set-ExecutionPolicy Unrestricted{Environment.NewLine}{Settings2.Default.psExec_Path} \\{pcName} -h -u {Environment.UserDomainName}\{Environment.UserName} -p {password} -d powershell -f {Settings2.Default.scriptFolder}\{pcName}_Settings.ps1";
                File.AppendAllText(localPath, settings);

                using (Process process = new Process())
                {
                    process.StartInfo.FileName = localPath;
                    process.Start();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show($@"Случилась непредвиденная ошибка: {e.Message}");
                File.AppendAllText($@"{Environment.CurrentDirectory}\Logs", $@"{DateTime.Now}:\r\n{e.Source}\r\n{e.Message}");
            }
        }
        private bool CreateSettings(string pcName, string settings)
        {
            if (XMLWorker.GetSettings() != "")
            {
                string psPath = $@"{Settings2.Default.scriptFolder}\{pcName}_Settings.ps1";
                FileInfo psFile = new FileInfo(psPath);//same for PS script
                if (psFile.Exists)
                {
                    psFile.Delete();

                }
                File.AppendAllText(psPath, settings);
                allPath.Add(psPath);
                return true;
            }
            else
                return false;
        }
        #endregion

        #region BIOS
        private void BiosSettingsInstalling(string pcName)
        {
            try
            {
                string localPath = $@"{Environment.CurrentDirectory}\{pcName}_bios.bat";
                string path = $@"{Settings2.Default.scriptFolder}\{pcName}_bios.ps1";
                string biosScript = $@"{Settings2.Default.psExec_Path} \\{pcName} -h -u \\{Environment.UserDomainName}\{Environment.UserName} -p {password} powershell -f {path}";
                allPath.Add(localPath);
                allPath.Add(path);
                File.AppendAllText(localPath, biosScript);

                using (Process process = new Process())
                {
                    process.StartInfo.FileName = localPath;
                    
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show($@"Случилась непредвиденная ошибка: {e.Message}");
                File.AppendAllText($@"{Environment.CurrentDirectory}\Logs", $@"{Environment.NewLine}{DateTime.Now}:{Environment.NewLine}{e.Source}{Environment.NewLine}{e.Message}");
            }
            
        }

        private void ResetBiosPassword(string pcName, string model)
        {
            if(XMLWorker.GetResetBiosPassScript(model) != "")
            {
                string path = $@"{Settings2.Default.scriptFolder}\{pcName}_resetPassword.ps1";
                allPath.Add(path);
                string localPath = $@"{Environment.CurrentDirectory}\{pcName}_reset.bat";
                allPath.Add(localPath);
                File.AppendAllText(localPath, $@"{Settings2.Default.psExec_Path} \\{pcName} -h -u \\{Environment.UserDomainName}\{Environment.UserName} -p {password} powershell -f {path}");
                File.WriteAllText(path, FindPassword(XMLWorker.GetResetBiosPassScript(model), biosPass, @"\w*Bpass\w*"));
                Process process = new Process();
                process.StartInfo.FileName = localPath;
                
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.Start();
            } 
        }

        private void SetBiosPassword(string pcName, string model)
        {
            string path = $@"{Settings2.Default.scriptFolder}\{pcName}_setPassword.ps1";
            if(XMLWorker.GetInstallingBiosPassScript(model) != "")
            {
                File.WriteAllText(path, FindPassword(XMLWorker.GetInstallingBiosPassScript(model), biosPass, @"\w*Bpass\w*"));
                allPath.Add(path);
                
                string localPath = $@"{Environment.CurrentDirectory}\{pcName}_setPass.cmd";
                allPath.Add(localPath);
                File.AppendAllText(localPath, $@"{Settings2.Default.psExec_Path} \\{pcName} -h -u \\{Environment.UserDomainName}\{Environment.UserName} -p {password} powershell -f {path}");

                Process process = new Process();
                process.StartInfo.FileName = localPath;
                process.EnableRaisingEvents = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.Exited += Finish;
                process.Start();
            }
            
            
        }

        private void Finish(object sender, EventArgs e)
        {
            Percent = 100;
        }

        private void CreateBiosSettings(string pcName, string settings)
        {
            string path = $@"{Settings2.Default.scriptFolder}\{pcName}_bios.ps1";
            allPath.Add(path);
            File.WriteAllText(path, settings);
        }
        #endregion

        private string FindPasswords(string script)//Replace keywords Apass(admin pass) and Bpass(bios pass)
        {
            Regex regexA = new Regex(@"\w*Apass\w*");
            Regex regexB = new Regex(@"\w*Bpass\w*");
            string result = regexA.Replace(script, admPass);
            result = regexB.Replace(result, biosPass);
            return result;
        }

        private string FindPassword(string script, string pass, string keyWord)
        {
            Regex regex = new Regex(keyWord);
            string result = regex.Replace(script, pass);
            return result;
        }

    }
}
