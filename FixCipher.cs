using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermedios.BasicBMP
{
    internal class FixCipher
    {
        public void Run()
        {
            string filePath = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image.bmp";

            using var fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);

            // Leer 2 bytes.
            int b1 = fs.ReadByte();
            int b2 = fs.ReadByte();

            if (b1 == -1 || b2 == -1)
            {
                Console.WriteLine("Archivo demasiado corto."); return;
            }

            // Volver al principio y escribir 'B', 'M'.
            fs.Seek(0, SeekOrigin.Begin);
            fs.WriteByte((byte)'B');
            fs.WriteByte((byte)'M');

            Console.WriteLine("Firma restaurada a 'BM'.");
        }
    }
}
