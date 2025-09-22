using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermedios.BasicBMP
{
    internal class CipherBMP
    {
        public void Run()
        {
            string filePath = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image.bmp";

            using (FileStream fs = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                char b1 = (char)fs.ReadByte();
                char b2 = (char)fs.ReadByte();

                if (b1 != 'B' || b2 != 'M')
                {
                    Console.WriteLine("No es un archivo BMP"); return;
                }
                else
                {
                    fs.Seek(0, SeekOrigin.Begin);
                    fs.WriteByte((byte)'M');
                    fs.WriteByte((byte)'B');


                    Console.WriteLine("El archivo ha sido cifrado.");
                }
            }
        }
    }
}
