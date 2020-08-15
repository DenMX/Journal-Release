using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;

namespace Journal_Release
{
    class DataBase
    {
        public static SqlConnection connection = null;

        private static List<string> models;

        public delegate void NewModel(string modelName);
        public static event NewModel CreateNewModel = delegate { };

        private static string _newModelName = "";
        private static string NewModelName
        {
            get { return _newModelName; }
            set
            {
                _newModelName = value;
                models.Add(value);
                CreateNewModel(_newModelName);
            }
        }

        
        public static async void LoadDB()
        {
            Comps.compsList.Clear();
            SqlDataReader reader = null;
            if (connection == null)
            {
                try
                {
                    connection = new SqlConnection(Settings2.Default.dataSource);
                    await connection.OpenAsync(); //Open connection with start programm and closing it with main window
                }
                catch
                {
                    MessageBox.Show("Не удалось установить соединение с базой. Возможно местоположение изменилось. Укажите еще раз.");
                    ChangePath();
                    return;
                }
            }

            SqlCommand command = new SqlCommand("SELECT * FROM [Table] ORDER BY PCname", connection);

            try
            {
                reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {

                    new Comps(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), Crypter.RSADecrypt(reader[4].ToString()), Crypter.RSADecrypt(reader[5].ToString()), reader[6].ToString());
                }

                
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }

            finally
            {
                if (reader != null)
                    reader.Close();
                if(models == null)
                {
                    models = new List<string>();
                    GetModels();
                }
                
            }

        }

        

        public static List<string> GetModelList()
        {
            if (models == null)
            {
                models = new List<string>();
                Task.Run(() => GetModels());
            }            
            return models;
        }

        private static async void GetModels()
        {
            SqlDataReader reader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Models]", connection);

            List<string> modelss = new List<string>();

            try
            {
                reader = await command.ExecuteReaderAsync();

                while(await reader.ReadAsync())
                {
                    modelss.Add(reader[0].ToString());
                }
                reader.Close();
                
                foreach(string s in modelss)
                {
                    ClearModelsString(s);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private static void ClearModelsString(string models)
        {
            
                string clear = "";
                foreach (char c in models)
                {
                    if (!char.IsWhiteSpace(c))
                    {
                        clear += c;
                    }
                }
                if(clear != "" && clear != null)
                    NewModelName = clear;
                
            
        }

        public static async void ChangePath()
        {
            if (connection != null)
            {
                connection.Close();
                connection = null;
            }
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "SQL DataBase(*.mdf) |*.mdf";
            open.Title = "Выберите документ для импорта данных";
            open.FilterIndex = 2;
            if (open.ShowDialog() == true)
            {
                Settings2.Default.dataSource = @" Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =" + open.FileName + "; Integrated Security = True";
                Settings2.Default.Save();
                connection = new SqlConnection(Settings2.Default.dataSource);
                await connection.OpenAsync();
            }
        }

        public static async void Add(string pcName, string admPass, string biosPass, string model) //Insert new PC
        {
            SqlCommand command = new SqlCommand("INSERT INTO [Table] (PcName, Date, Responsibility, AdmPass, BiosPass, pcModel)VALUES(UPPER(@PcName), @Date, @Responsible, @AdmPass, @BiosPass, @Model)", connection);
            command.Parameters.AddWithValue("PcName", pcName);
            command.Parameters.AddWithValue("Date", DateTime.Today);
            command.Parameters.AddWithValue("Responsible", Environment.UserName);
            command.Parameters.AddWithValue("AdmPass", Crypter.RSAEncrypt(admPass));
            command.Parameters.AddWithValue("BiosPass", Crypter.RSAEncrypt(biosPass));
            command.Parameters.AddWithValue("Model", model);

            try
            {
                await command.ExecuteNonQueryAsync();
                
                UpdateLastPC();

                MessageBox.Show("Запись добавлена успешно.");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            
        }

        private static async void UpdateLastPC()
        {
            SqlDataReader reader = null;
            SqlCommand sqlCommand = new SqlCommand("SELECT MAX(Id) FROM [Table]");
            
            while (connection.State == System.Data.ConnectionState.Open)
                await Task.Delay(1000);

            try
            {
                reader = await sqlCommand.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {

                    Comps comp = new Comps(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), Crypter.RSADecrypt(reader[4].ToString()), Crypter.RSADecrypt(reader[5].ToString()), reader[6].ToString());

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public static async void AddNewModel(string modelName)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [Models] (Model)VALUES(@Model)", connection);
            command.Parameters.AddWithValue("Model", modelName);
            try
            {
                await command.ExecuteNonQueryAsync();
                NewModelName = modelName;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                File.AppendAllText($@"{Environment.CurrentDirectory}\Logs", $"\r\n{DateTime.Now}:{e.Message}");
            }
        }

        internal static void CloseConnection()
        {
            if(connection != null)
                connection.Close();
        }

        public static async void Update(string pcName, string admPass, string biosPass, string model)//Update PC if it was exist
        {
            if(model == "" || model == null)
            {
                MessageBox.Show("Необходимо указать модель компьютера.");
                return;
            }
            SqlCommand command = new SqlCommand("UPDATE [Table] SET PcName=@PcName, Date=@Date, Responsibility=@Responsibility, AdmPass=@AdmPass, BiosPass=@BiosPass, pcModel=@Model WHERE PcName=@PcName", connection);
            command.Parameters.AddWithValue("PcName", pcName.ToUpper());
            command.Parameters.AddWithValue("Date", DateTime.Today);
            command.Parameters.AddWithValue("Responsibility", Environment.UserName);
            command.Parameters.AddWithValue("AdmPass", Crypter.RSAEncrypt(admPass));
            command.Parameters.AddWithValue("BiosPass", Crypter.RSAEncrypt(biosPass));
            command.Parameters.AddWithValue("Model", model);
            

            try
            {
                await command.ExecuteNonQueryAsync();
                MessageBox.Show("Запись обновлена успешно.");
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public static async void ImportDB(string connect)
        {

            SqlConnection con = new SqlConnection(@" Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =" + connect + "; Integrated Security = True"); //Connection to second DB

            SqlDataReader reader = null;

            await con.OpenAsync();

            SqlCommand SelectCommand = new SqlCommand("SELECT * FROM [Table]", con);

            Comps.compsList.Clear();
            reader = await SelectCommand.ExecuteReaderAsync();

            //Reading all DB and get list
            while (await reader.ReadAsync())
            {
                Comps comp = new Comps();
                comp.PcName = reader[1].ToString();
                comp.Responsibility = reader[3].ToString();
                try
                {
                    comp.AdmPass = Crypter.RSADecrypt(reader[4].ToString());
                    comp.BiosPass = Crypter.RSADecrypt(reader[5].ToString());
                    comp.Date = reader[2].ToString();
                }
                catch
                {
                    comp.AdmPass = reader[4].ToString();
                    comp.BiosPass = reader[5].ToString();
                    comp.Date = null;
                }
                Comps.compsList.Add(comp);
            }

            reader.Close();
            con.Close();

            SqlCommand command = new SqlCommand("INSERT INTO [Table] (PcName, Date, Responsibility, AdmPass, BiosPass)VALUES(UPPER(@PcName), @Date, @Responsible, @AdmPass, @BiosPass)", connection);
            
            foreach (Comps comp in Comps.compsList)
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("PcName", comp.PcName);
                if(comp.Date != null && comp.Date.Length > 1)
                {
                    command.Parameters.AddWithValue("Date", DateTime.Parse(comp.Date));
                }
                else
                {
                    command.Parameters.AddWithValue("Date", DBNull.Value);
                }
                command.Parameters.AddWithValue("Responsible", comp.Responsibility);
                command.Parameters.AddWithValue("AdmPass", Crypter.RSAEncrypt(comp.AdmPass));
                command.Parameters.AddWithValue("BiosPass", Crypter.RSAEncrypt(comp.BiosPass));
                command.ExecuteNonQuery();
            }
        }

        public static async void ImportFromWord(string pcName, string responsible, string admPass, string biosPass)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [Table] (PcName, Responsibility, AdmPass, BiosPass)VALUES(UPPER(@PcName), @Responsible, @AdmPass, @BiosPass)", connection);
            command.Parameters.AddWithValue("PcName", pcName);
            command.Parameters.AddWithValue("Responsible", responsible);
            command.Parameters.AddWithValue("AdmPass", admPass);
            command.Parameters.AddWithValue("BiosPass", biosPass);

            await command.ExecuteNonQueryAsync();
        }

        public static async void SqlWindow(string command)
        {
            SqlCommand command1 = new SqlCommand(command, connection);

            string check = null;

            if (command != null)
            {
                for (int i = 0; i < 6; i++)
                {
                    check += command[i];
                }
                if (check == "SELECT")
                {
                    MessageBox.Show("Нельзя выполнять операцию SELECT в текущем окне");
                    return;
                }
                else
                {
                    try
                    {
                        await command1.ExecuteNonQueryAsync();
                        MessageBox.Show("Запрос успешно выполнен");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show($"Произошла ошибка при выполнении запроса: {e}");
                    }
                }
            }
        }
    }
}