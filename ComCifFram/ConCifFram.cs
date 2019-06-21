using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ComCifFram
{
    public class ConCifFram : iConCifFram
    {
        private BinaryReader binaryReader;

        bool iConCifFram.ComprimeArchivo(string llave, string rutaIn, string rutaOut)
        {
            bool result = false;            
            try
            {
                if (ComprimeArchivo(llave, rutaIn, rutaOut))
                    result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        bool iConCifFram.DescomprimeArchivo(string llave, string rutaIn, string rutaOut)
        {
            bool result = false;            
            try
            {
                if (DescomprimirArchivo(llave, rutaIn, rutaOut))
                    result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        private bool ComprimeArchivo(string llave, string rutaIn, string rutaOut)
        {
            bool result = false;
            string textoPlano = string.Empty;
            string TextoComprimido = null;
            string textoCifrado = string.Empty;
            try
            {
                using (FileStream fileStream = File.Open(rutaIn, FileMode.Open))
                {
                    binaryReader = new BinaryReader(fileStream);
                    textoPlano = Encoding.UTF8.GetString(binaryReader.ReadBytes((int)fileStream.Length));
                    binaryReader.Close();
                    fileStream.Close();
                }

                if (compactor.Comprime(textoPlano, out TextoComprimido))
                {
                    if (encryptor.CifrarTexto(llave, TextoComprimido, out textoCifrado))
                    {
                        if (GuardarArchivo(rutaOut, Encoding.UTF8.GetBytes(textoCifrado)))
                        {
                            result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        private bool DescomprimirArchivo(string llave, string rutaIn, string rutaOut)
        {
            bool result = false;
            string textoPlano = string.Empty;
            string TextoComprimido = null;
            string textoCifrado = string.Empty;
            try
            {
                using (FileStream fileStream = File.Open(rutaIn, FileMode.Open))
                {
                    binaryReader = new BinaryReader(fileStream);
                    textoCifrado = Encoding.UTF8.GetString(binaryReader.ReadBytes((int)fileStream.Length));
                    binaryReader.Close();
                    fileStream.Close();
                }

                if (encryptor.DescifrarTexto(llave, textoCifrado, out TextoComprimido))
                {
                    if (compactor.Descomprime(TextoComprimido.ToString(), out textoPlano))
                    {
                        if (GuardarArchivo(rutaOut, Encoding.UTF8.GetBytes(textoPlano)))
                        {
                            result = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        private static bool GuardarArchivo(string rutaArchivo, byte[] registros)
        {
            bool result = false;
            try
            {
                if (File.Exists(rutaArchivo))
                {
                    throw new Exception(string.Format("El archivo ya existe: {0}", rutaArchivo));
                }
                using (FileStream fileStream = new FileStream(rutaArchivo, FileMode.CreateNew, FileAccess.Write))
                {
                    fileStream.Write(registros, 0, registros.Length);
                    fileStream.Close();
                }
                result = true;
            }
            catch (Exception)
            {
            }
            return result;
        }
    }
}
