using System;
using System.Text;
using System.Security.Cryptography;
using System.Windows;

namespace Journal_Release
{
    class Crypter
    {
        #region MD5
        //Public method to check password
        public static bool CheckPass(string pass)
        {

            using (MD5 md5Hash = MD5.Create())
            {
                if (VerifyMd5Hash(md5Hash, pass, Settings2.Default.password) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public static void changePass(string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                Settings2.Default.password = GetMd5Hash(md5Hash, password);
                Settings2.Default.Save();
            }

        }

        //Getting hash
        private static string GetMd5Hash(MD5 md5Hash, string input)
        {

            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }


            return sBuilder.ToString();
        }

        public static string GetHash(byte[] first)//to compare some files
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(first);
            StringBuilder stringBuilder = new StringBuilder();

            for(int i = 0; i< data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        //Verify md5 hash
        public static bool VerifyMd5Hash(MD5 md5Hash, string input, string hash)
        {
            string hashOfInpupt = GetMd5Hash(md5Hash, input);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInpupt, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region RSA

        private static string key ;

        public static string RSAEncrypt(string DataToEncrypt)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    key = RSA.ToXmlString(true); //Stop here and save value to var "key" then delete this line. Also you may keep that in settings.
                    RSA.FromXmlString(key);

                    encryptedData = RSA.Encrypt(Encoding.UTF8.GetBytes(DataToEncrypt), false);
                }
                return Convert.ToBase64String(encryptedData);
            }
            catch (CryptographicException e)
            {
                MessageBox.Show($"Ошибка шифрования: {DataToEncrypt} : {e}");
                return null;
            }
        }

        public static string RSADecrypt(string DataToDecrypt)
        {
            try
            {
                byte[] decryptedData;

                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.FromXmlString(key);

                    decryptedData = RSA.Decrypt(Convert.FromBase64String(DataToDecrypt), false);
                }
                return Encoding.UTF8.GetString(decryptedData);
            }
            catch (CryptographicException e)
            {
                return DataToDecrypt;
            }
        }
        #endregion
    }
}
