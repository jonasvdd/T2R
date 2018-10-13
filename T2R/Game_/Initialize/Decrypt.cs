using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace T2R.Initialize
{
    public class Decrypt
    {
        public Decrypt(string path, List<string> data)
        {
            // zo een goed mogelijke kopie van wat er in de slides staat
            AesManaged algo = new AesManaged();
            string passwd = "zeventig_min_één";
            byte[] bytePwd = Encoding.ASCII.GetBytes(passwd);
            SHA256 hash = SHA256Managed.Create();
            byte[] key = hash.ComputeHash(bytePwd);
            byte[] iv = hash.ComputeHash(key);
            algo.KeySize = 128;
            byte[] Key = new byte[algo.Key.Length];
            byte[] IV = new byte[algo.IV.Length];
            for (int iv0 = 0; iv0 < algo.Key.Length; ++iv0)
                Key[iv0] = key[iv0];
            for (int iv00 = 0; iv00 < algo.IV.Length; ++iv00)
                IV[iv00] = iv[iv00];
            algo.Key = Key;
            algo.IV = IV;
            if (algo != null && path != null)
            {
                new Decrypt(path, algo, data);
            }
            else
            {
                MessageBox.Show("Error, de tekst dan niet weergegeven worden");
                throw new InvalidDataException("The datapaths is wrong, or you can't decrypt teh data");
            }
        }

        private Decrypt(string path, AesManaged algo, List<string> data)
        {
            ICryptoTransform decrypto = algo.CreateDecryptor();
            using (FileStream fsin = new FileStream(path, FileMode.Open))
            using (MemoryStream msOut = new MemoryStream(100))
            using (CryptoStream sDecrypt = new CryptoStream(fsin, decrypto, CryptoStreamMode.Read))
            using (BinaryReader fsbr = new BinaryReader(msOut))
            {
                sDecrypt.CopyTo(msOut);
                long pos = msOut.Seek(0, SeekOrigin.Begin);
                while (msOut.Position < msOut.Length)
                {
                    string text = fsbr.ReadString();
                    //Console.WriteLine(text);
                    data.Add(text);
                }
            }
        }
    }
}
