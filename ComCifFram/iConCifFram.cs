using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComCifFram
{
    public interface iConCifFram
    {
        bool ComprimeArchivo(string llave, string rutaIn, string rutaOut);

        bool DescomprimeArchivo(string llave, string rutaIn, string rutaOut);
    }
}
