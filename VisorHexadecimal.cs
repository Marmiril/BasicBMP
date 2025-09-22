using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Intermedios.BasicBMP
{

    /// <summary>
    /// Visor hexadecimal en C# que muestra el contenido de un archivo binario en
    /// pantalla de la siguiente manera:
    /// - 16 bytes por cada fila
    /// - 24 filas en pantalla
    /// Los 16 bytes se muestran primero en hexadecimal y luego como caracteres imprimibles.
    /// sustituye los bytes menores a 32 (caracteres no imprimibles) por puntos.
    /// Muestra en pantalla 24 filas de 16 bytes cada vez que el usuario presiona Enter.
    /// </summary>
    internal class VisorHexadecimal
    {
        public void Run()
        {
            const int bufferSize = 16;

            string fileName = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image.bmp";

            using (FileStream file = File.OpenRead(fileName))
            {
                byte[] data = new byte[bufferSize];

                int cantidad;
                int c = 0;
                string linea = string.Empty;

                do
                {
                    Console.Write(ToHex(file.Position, 8));
                    Console.Write("  ");

                    cantidad = file.Read(data, 0, bufferSize);

                    for (int i = 0; i < cantidad; i++)
                    {
                        Console.Write(ToHex(data[i], 2) + " ");

                        if (data[i] < 32)
                        {
                            linea += ".";
                        }
                        else
                        {
                            linea += (char)data[i];
                        }
                    }

                    if (cantidad < bufferSize)
                    {
                        for (int i = cantidad; i < bufferSize; i++)
                        {
                            Console.Write("   ");
                        }
                    }

                    Console.WriteLine(linea);
                    linea = "";

                    c++;

                    if (c == 24)
                    {
                        Console.ReadLine();
                        c = 0;
                    }
                }
                while (cantidad == bufferSize);
            }
        }

        public static string ToHex(long n, int digits)
        {
            string hex = Convert.ToString(n, 16);
            
           while(hex.Length < digits)
            {
                hex = "0" + hex;
            }
            return hex;
        }
    }
}
