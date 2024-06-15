using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class EncryptDecrypt
    {
            private byte[] key = { };
            private byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            public string Decrypt(string stringToDecrypt, string sEncryptionKey)
            {
                byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
                try
                {
                    key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    inputByteArray = Convert.FromBase64String(stringToDecrypt.Replace(" ", "+"));
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                    return encoding.GetString(ms.ToArray());
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            public string Encrypt(string stringToEncrypt, string SEncryptionKey)
            {
                try
                {
                    key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey);
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }

            //public String EncryptPassword(String password)
            //{
            //    SymmCrypto tds = new SymmCrypto(SymmCrypto.SymmProvEnum.Rijndael);
            //    String strTempPass = "";
            //    if (tds.Encrypting(password, "123") == "")
            //    {
            //        strTempPass = tds.Encrypting(password, "111");
            //    }
            //    else
            //    {
            //        strTempPass = tds.Encrypting(password, "123");
            //    }
            //    return strTempPass;
            //}
        }
}
