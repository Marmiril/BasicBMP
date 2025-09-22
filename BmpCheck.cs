using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermedios.BasicBMP
{

    /// <summary>
    /// Abrir un archivo, leer los 2 primeros bytes y verificar que sean B y M (ASCII).
    /// Si no, no es un BMP “clásico” de Windows.
    /// </summary>
    internal class BmpCheck
    {
        public void Run()
        {
            string filePath = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image.bmp";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("El archivo no existe."); return;
            }

            byte[] sig = new byte[2];

            try
            {
                using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                int read = fs.Read(sig, 0, 2);

                if (read < 2)
                {
                    Console.WriteLine("Archivo demasiado pequeño para BMP."); return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo: {ex.Message}.");
            }

            bool isBmp = (sig[0] == (byte)'B' && sig[1] == (byte)'M');

            Console.WriteLine(isBmp
                ? "Es un archivo BMP pues tiene firma 'BM'."
                : "No tiene firma 'BM' ergo no es BMP.");
        }
    }
}
