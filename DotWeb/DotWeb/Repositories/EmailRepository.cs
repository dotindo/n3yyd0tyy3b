using System.Collections.Generic;
using System.Linq;
using DotWeb.Models;
using System;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace DotWeb.Repositories
{
    public class EmailRepository
    {
        public bool Email(string penerima, string subject, string message)
        {
            bool exe = false;
            try
            {
                if (string.IsNullOrEmpty(penerima) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(message))
                    return exe;

                using (AppDb context = new AppDb())
                {
                    NotificationEmail data = new NotificationEmail()
                    {
                        To = penerima,
                        SubjectEmail = subject,
                        Email = message
                    };
                    context.NotificationEmails.Add(data);
                    context.SaveChanges();
                    exe = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return exe;
        }
        public static string getSMTPHost()
        {
            using (AppDb context = new AppDb())
            {
                return context.SMTPConfigs.Select(p => p.Host).FirstOrDefault();
            }
        }
        public static string getSMTPUsername()
        {
            using (AppDb context = new AppDb())
            {
                return  context.SMTPConfigs.Select(u => u.Username).FirstOrDefault();                
            }
        }
        public static string getSMTPPassword()
        {
            using (AppDb context = new AppDb())
            {
                string result = context.SMTPConfigs.Select(u => u.Password).FirstOrDefault();
                return Decryptdata(result);
            }
        }
        public static string getSMTPSalt()
        {
            using (AppDb context = new AppDb())
            {
                return context.SMTPConfigs.Select(p => p.Salt).FirstOrDefault();
            }
        }
        public static int getSMTPPort()
        {
            using (AppDb context = new AppDb())
            {
                return context.SMTPConfigs.Select(p => p.Port).FirstOrDefault();
            }
        }
        public static bool SMTPIsUseSSL()
        {
            using (AppDb context = new AppDb())
            {
                return context.SMTPConfigs.Select(p => p.IsUseSSL).FirstOrDefault();
            }
        }

        private static string Decryptdata(string encryptedResult)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(encryptedResult);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(getSMTPSalt());
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
        
            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);
        
            string decryptedResult = Encoding.UTF8.GetString(bytesDecrypted);

            return decryptedResult;
        }
        private static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
        
            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 2, 1, 7, 3, 6, 4, 8, 5 };
        
            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
        
                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
        
                    AES.Mode = CipherMode.CBC;
        
                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }
        
            return decryptedBytes;
        }
        
    }
}

