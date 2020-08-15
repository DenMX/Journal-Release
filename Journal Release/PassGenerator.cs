using System;
using System.Linq;
using System.Windows.Controls;

namespace Journal_Release
{
    class PassGenerator
    {
        private Random random = new Random();

        private static string admpass = "qwertyuiopasdfghjklzxcvbnm1234567890QWERTYUIOPASDFGHJKLZXCVBNM"; //Symbols for password
        private static string biospass = "qwertyuiopasdfghjklzxcvbnm1234567890";
        private static Random rand = new Random();

        //Generate admin pass
        public static void GetPass(TextBox admPass, TextBox biosPass)
        {
            //Generating Admin pass
            string admPassRes = "";
            bool isNums = false; //Counts of numbers in password
            while (isNums == false)
            {
                admPassRes = "";
                for (int i = 0; i <= Settings2.Default.admPassLenght; i++)
                {

                    admPassRes += admpass[rand.Next(0, admpass.Length-1)];
                }
                int count;
                int.TryParse(string.Join("", admPassRes.Where(c => char.IsDigit(c))), out count);
                if (count > 0)
                {
                    isNums = true;
                }
            }
            admPass.Text = admPassRes;
            isNums = false;

            //Generating bios pass
            string biosPassRes = "";
            //bool isNums = false; //Counts of numbers in password
            while (isNums == false)
            {
                biosPassRes = "";
                for (int i = 0; i <= Settings2.Default.biosPassLenght; i++)
                {

                    biosPassRes += biospass[rand.Next(0, biospass.Length-1)];
                }
                int count;
                int.TryParse(string.Join("", biosPassRes.Where(c => char.IsDigit(c))), out count);
                if (count > 0)
                {
                    isNums = true;
                    break;
                }
            }
            biosPass.Text = biosPassRes;
        }

        //Generate bios pass
        public static string GetBiosPass()
        {
            string passRes = "";
            bool isNums = false; //Counts of numbers in password
            while (isNums == false)
            {
                passRes = "";
                for (int i = 0; i <= Settings2.Default.biosPassLenght; i++)
                {

                    passRes += biospass[rand.Next(0, biospass.Length-1)];
                }
                int count;
                int.TryParse(string.Join("", passRes.Where(c => char.IsDigit(c))), out count);
                if (count > 0)
                {
                    isNums = true;
                    break;
                }
            }

            return passRes;
        }

        public static string GetAdmPass()
        {
            string passRes = "";
            bool isNums = false; //Counts of numbers in password
            while (isNums == false)
            {
                passRes = "";
                for (int i = 0; i <= Settings2.Default.admPassLenght; i++)
                {

                    passRes += admpass[rand.Next(0, admpass.Length-1)];
                }
                int count;
                int.TryParse(string.Join("", passRes.Where(c => char.IsDigit(c))), out count);
                if (count > 0)
                {
                    isNums = true;
                }
            }
            return passRes;
        }
    }
}
