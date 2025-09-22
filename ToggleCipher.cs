using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermedios.BasicBMP
{
    internal class ToggleCipher
    {
        public void Run()
        {
            string filePath = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image.bmp";

            try
            {
                using var fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);

                int b1 = fs.ReadByte();
                int b2 = fs.ReadByte();
                
                if (b1 == -1 || b2 == -1)
                {
                    Console.WriteLine("Archivo demasiado corto."); return;
                }

                fs.Seek(0, SeekOrigin.Begin);

                if (b1 == 'B' && b2 == 'M')
                {
                    fs.WriteByte((byte)'M');
                    fs.WriteByte((byte)'B');
                    Console.WriteLine("Firma alterada.");
                }
                else if (b1 == 'M' && b2 == 'B')
                {
                    fs.WriteByte((byte)'B');
                    fs.WriteByte((byte)'M');
                    Console.WriteLine("Firma restaurada.");
                }
                else
                {
                    Console.WriteLine("Las dos primeras posiciones no son ni 'BM' ni 'MB'.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }

        }
    }
}
