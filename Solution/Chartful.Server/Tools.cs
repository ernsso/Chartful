using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Chartful.Server
{
    public static class Tools
    {
        public enum LogType { Info, Error };

        public static void Log(LogType logType, String message)
        {
            try
            {
                string logDirectory = Environment.CurrentDirectory + @"\Logs";
                if (!Directory.Exists(logDirectory))
                    Directory.CreateDirectory(logDirectory);
                StreamWriter file = new StreamWriter(logDirectory + @"\log_" + String.Format("{0:yyyy-MM-dd}", DateTime.Today) + ".txt", true);
                file.WriteLine(String.Format("{0} {1} : {2}", DateTime.Now, logType.ToString(), message));
                file.Close();
                Console.WriteLine(String.Format("{0} {1} : {2}", DateTime.Now, logType.ToString(), message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        public static String GenerateRandom(Int32 lenght)
        {
            return GenerateRandom(lenght, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray());
        }

        public static String GenerateRandom(Int32 lenght, Char[] chars)
        {
            Char[] result = new Char[lenght];
            Random random = new Random();
            for (Int32 i = 0; i < lenght; i++)
                result[i] = chars[random.Next(chars.Length)];
            return new String(result);
        }

        private static byte[] GenerateHash(string input)
        {
            HashAlgorithm algorithm = SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
        }

        public static string GetHash(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GenerateHash(input))
                sb.Append(b.ToString("X2")); //X2 est le spécificateur de format hexadecimal

            return sb.ToString();
        }
    }
}