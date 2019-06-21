using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ComCifFram
{
    internal class encryptor
    {
        internal static bool CifrarTexto(string llave, string textoPlano, out string textoCifrado)
        {
            bool result = false;
            byte[] inArray = null;
            textoCifrado = string.Empty;
            try
            {
                if (Cifrar(llave, textoPlano, out inArray))
                {
                    textoCifrado = Convert.ToBase64String(inArray);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        internal static bool DescifrarTexto(string llave, string textoCifrado, out string textoPlano)
        {
            bool result = false;
            textoPlano = string.Empty;
            string empty = string.Empty;
            try
            {
                byte[] textoArray = Convert.FromBase64String(textoCifrado);
                if (Descifrar(llave, textoArray, out empty))
                {
                    textoPlano = empty;
                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }
            return result;
        }

        private static bool Cifrar(string llave, string textoPlano, out byte[] textoCifrado)
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            bool result = false;
            byte[] key = null;
            textoCifrado = null;
            try
            {
                if (!ObtenerLlave(llave, out key))
                {
                    throw new Exception("Error al calcular la llave...");
                }
                rijndaelManaged.Mode = CipherMode.ECB;
                rijndaelManaged.BlockSize = 256;
                rijndaelManaged.Padding = PaddingMode.Zeros;
                rijndaelManaged.GenerateIV();
                rijndaelManaged.Key = key;
                byte[] bytes = Encoding.UTF8.GetBytes(textoPlano);
                ICryptoTransform transform = rijndaelManaged.CreateEncryptor(rijndaelManaged.Key, rijndaelManaged.IV);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
                cryptoStream.Write(bytes, 0, bytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] array = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                textoCifrado = array;
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        private static bool Descifrar(string llave, byte[] textoCifrado, out string textoDecifrado)
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            bool result = false;
            byte[] key = null;
            textoDecifrado = string.Empty;
            try
            {
                if (!ObtenerLlave(llave, out key))
                {
                    throw new Exception("Error al calcular la llave...");
                }
                rijndaelManaged.Mode = CipherMode.ECB;
                rijndaelManaged.BlockSize = 256;
                rijndaelManaged.Padding = PaddingMode.Zeros;
                rijndaelManaged.GenerateIV();
                rijndaelManaged.Key = key;
                ICryptoTransform transform = rijndaelManaged.CreateDecryptor(rijndaelManaged.Key, rijndaelManaged.IV);
                MemoryStream memoryStream = new MemoryStream(textoCifrado);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
                StreamReader streamReader = new StreamReader(cryptoStream);
                textoDecifrado = streamReader.ReadToEnd();
                memoryStream.Close();
                cryptoStream.Close();
                streamReader.Close();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                result = false;
            }
            return result;
        }

        private static bool ObtenerLlave(string llave, out byte[] llaveFinal)
        {
            MD5 mD = new MD5CryptoServiceProvider();
            StringBuilder stringBuilder = new StringBuilder();
            bool result = false;
            llaveFinal = null;
            try
            {
                byte[] bytes = Encoding.UTF8.GetBytes(llave);
                byte[] array = mD.ComputeHash(bytes);
                for (int i = 0; i < array.Length; i++)
                {
                    stringBuilder.Append(array[i].ToString("x2"));
                }
                llaveFinal = array;
                llaveFinal = Encoding.UTF8.GetBytes(stringBuilder.ToString());
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
    }
}
