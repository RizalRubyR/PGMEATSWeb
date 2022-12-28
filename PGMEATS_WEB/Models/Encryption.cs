using System;
using System.Security.Cryptography;

namespace ADLESKAP.Models
{
    public class Encryption
    {
        private TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();

        public Encryption()
        {
            string key = "TOS";
            // Initialize the crypto provider.
            TripleDes.Key = TruncateHash(key, TripleDes.KeySize / 8);
            TripleDes.IV = TruncateHash("", TripleDes.BlockSize / 8);
        }

        private byte[] TruncateHash(string key, int length)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();

            // Hash the key. 
            byte[] keyBytes = System.Text.Encoding.Unicode.GetBytes(key);
            byte[] hash = sha1.ComputeHash(keyBytes);
            var oldHash = hash;
            hash = new byte[length - 1 + 1];

            // Truncate or pad the hash. 
            if (oldHash != null)
                Array.Copy(oldHash, hash, Math.Min(length - 1 + 1, oldHash.Length));
            return hash;
        }

        public string EncryptData(string plaintext)
        {

            // Convert the plaintext string to a byte array. 
            byte[] plaintextBytes = System.Text.Encoding.Unicode.GetBytes(plaintext);

            // Create the stream. 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            // Create the encoder to write to the stream. 
            CryptoStream encStream = new CryptoStream(ms, TripleDes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            // Use the crypto stream to write the byte array to the stream.
            encStream.Write(plaintextBytes, 0, plaintextBytes.Length);
            encStream.FlushFinalBlock();

            // Convert the encrypted stream to a printable string. 
            return Convert.ToBase64String(ms.ToArray());
        }

        public string DecryptData(string encryptedtext)
        {

            // Convert the encrypted text string to a byte array. 
            byte[] encryptedBytes = Convert.FromBase64String(encryptedtext);

            // Create the stream. 
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            // Create the decoder to write to the stream. 
            CryptoStream decStream = new CryptoStream(ms, TripleDes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);

            // Use the crypto stream to write the byte array to the stream.
            decStream.Write(encryptedBytes, 0, encryptedBytes.Length);
            decStream.FlushFinalBlock();

            // Convert the plaintext stream to a string. 
            return System.Text.Encoding.Unicode.GetString(ms.ToArray());
        }
    }
}



//using System.IO;
//using System.Text;
//using System.Data;
//using System.Configuration;
//using System.Security.Cryptography;

//namespace ADLESKAP.Models
//{
//    public class Encryption
//    {
//        public string Encrypt(string clearText, string EncryptionKey = "TOS")
//        {
//            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
//            using (Aes encryptor = Aes.Create())
//            {
//                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
//                encryptor.Key = pdb.GetBytes(32);
//                encryptor.IV = pdb.GetBytes(16);
//                using (MemoryStream ms = new MemoryStream())
//                {
//                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
//                    {
//                        cs.Write(clearBytes, 0, clearBytes.Length);
//                        cs.Close();
//                    }
//                    clearText = System.Convert.ToBase64String(ms.ToArray());
//                }
//            }
//            return clearText;
//        }

//        public string Decrypt(string cipherText, string EncryptionKey = "TOS")
//        {
//            if (cipherText.Length < 5)
//            {
//                return cipherText;
//            }
//            byte[] cipherBytes = System.Convert.FromBase64String(cipherText);
//            using (Aes encryptor = Aes.Create())
//            {
//                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
//                encryptor.Key = pdb.GetBytes(32);
//                encryptor.IV = pdb.GetBytes(16);
//                using (MemoryStream ms = new MemoryStream())
//                {
//                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
//                    {
//                        cs.Write(cipherBytes, 0, cipherBytes.Length);
//                        cs.Close();
//                    }
//                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
//                }
//            }
//            return cipherText;
//        }
//    }
//}