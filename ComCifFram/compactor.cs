using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace ComCifFram
{
    internal class compactor
    {
        internal static bool Comprime(string textoPlano, out string TextoComprimido)
        {
            bool result = false;
            TextoComprimido = string.Empty;
            byte[] bytesArray = null;
            try
            {
                bytesArray = Zip(textoPlano);
                TextoComprimido = Convert.ToBase64String(bytesArray);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        internal static bool Descomprime(string TextoComprimido, out string textDescomprimido)
        {
            bool result = false;
            textDescomprimido = string.Empty;
            byte[] bytesArray = null;
            try
            {
                TextoComprimido = TextoComprimido.Replace("\0", "");
                bytesArray = Convert.FromBase64String(TextoComprimido);
                textDescomprimido = Unzip(bytesArray);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static byte[] Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }

        public static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        private static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }
    }
}
