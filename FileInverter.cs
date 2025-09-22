using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermedios.BasicBMP
{
    internal class FileInverter
    {
        public void Run()
        {
            string filePath = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image.bmp";
            string outPath  = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image.inv";
            try
            { /*
                // using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                // byte[] buffer = new byte[fs.Length];

                byte[] buffer = File.ReadAllBytes(filePath);

                // fs.Read(buffer, 0, buffer.Length);

                using(FileStream outFile = File.Create(outPath))
                {
                    for (long i = buffer.Length -1; i >= 0; i--)
                    {
                        outFile.WriteByte(buffer[i]);
                    }
                }
                */

                // Versión ultra reducida.

                byte[] buffer = File.ReadAllBytes(filePath);
                Array.Reverse(buffer);
                File.WriteAllBytes(outPath, buffer);

                Console.WriteLine($"Archivo invertido creado en:\n{outPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }
        }
    }
}
