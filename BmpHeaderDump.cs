using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermedios.BasicBMP
{
    internal class BmpHeaderDump
    {
        public void Run()
        {
            string filePath = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image.bmp";

            try
            {
                using var fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);

                Console.WriteLine("Pos | Valor hex | Carácter");
                Console.WriteLine("--------------------------");

                for (int i = 0; i < 16; i++)
                {
                    int value = fs.ReadByte();
                    if (value == -1) break; // -1 es fin del archivo.

                    char c = (value >= 32 && value <= 126) ? (char)value : '.';  // Recuerda ASCII imprimible.
                    Console.WriteLine($"{i,3} | 0x{value:X2}      | {c}");       // $"0x{value:X2}" → value en hexadecimal (X) con 2 dígitos (2).
                }



            }catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }
        }
    }
}
