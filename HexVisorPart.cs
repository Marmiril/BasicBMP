using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Intermedios.BasicBMP
{
    internal class HexVisorPart
    {
        public void Run()
        {
            string filePath = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image.bmp";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("El archivo no existe."); return;
            }

            const int bytes_linea = 16;
            const int lineas_pagina = 24;

            // Abriendo el archivo que existe.
            using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            int contador = 0;

            // Creando array de bytes para datos del archivo.
            byte[] buffer = new byte[bytes_linea]; // De 16 en 16.

            while (true)
            {
                long offset = fs.Position;  // Offset de inicio de línea.

                int read = fs.Read(buffer, 0, bytes_linea);

                if (read == 0) break; // Es el fin del archivo.

                // Offset en 8 dígitos hex (estilo visor).
                Console.Write($"{offset:x8}  ");

                // Console.WriteLine("Primeros bytes del archivo: ");

                for (int i = 0; i < read; i++)
                {
                    if (i > 0) Console.Write(' ');
                    Console.Write($"{buffer[i]:x2}");
                }


                // Si la última línea tiene menos de 16 bytes se alinea así. /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                /*
                Cada byte = 2 dígitos hex + 1 espacio = 3 caracteres.
                Pero al primer byte no le ponemos espacio delante, así que quitamos 1.
                Fórmula: (5 * 3 - 1) = 14.
                */
                int hexPrintedChars = (read == 0) ? 0 : (read * 3 - 1);

                /*
                 Cada byte ocupa 2 caracteres en hexadecimal.
                 Además, entre cada byte (menos el primero) hay un espacio.
                 Si hay 16 bytes: 
                    Hex: 16 * 2 = 32 caracteres.
                    Espacios: 15 espacios.
                    Total = 47 caracteres.
                    Por eso escribí la fórmula 16 * 3 - 1.
                    (16 * 3) sería “dos dígitos + un espacio” por cada byte.
                    Como al primero no se le pone espacio delante, se resta 1.
                 */
                int hexFullLineChars = 16 * 3 - 1;

                /*
                hexPrintedChars → cuántos caracteres hemos impreso en la parte hex real de esa línea.
                hexFullLineChars → cuántos caracteres debería ocupar la parte hex si la línea estuviera completa (47 en el caso de 16 bytes).
                hexFullLineChars - hexPrintedChars → cuántos espacios faltan para llegar a esa longitud estándar.
                new string(' ', …) → crea una cadena con tantos espacios como falten.
                Console.Write(...) → imprime esos espacios y así se alinea la columna ASCII.
                */
                if (hexPrintedChars < hexFullLineChars)
                {
                    Console.Write(new string(' ', hexFullLineChars - hexPrintedChars));
                }

                // Separador.
                Console.Write("   ");

                // La parte de ASCII.
                var sbAscii = new StringBuilder();

                for (int i = 0; i < read; i++)
                {
                    byte b = buffer[i];
                    char c = (b >= 32 && b <= 126) ? (char)b : '.';
                    sbAscii.Append(c);
                }
                Console.WriteLine(sbAscii.ToString());

                // Control para pasar páginas y no salga todo a mansalva.
                contador++;

                if (contador >= lineas_pagina && fs.Position < fs.Length)
                {
                    Console.WriteLine("\n Pulse enter para continuar o ESC para salir.");

                    while (true)
                    {
                        var key = Console.ReadKey(intercept: true);
                        if (key.Key == ConsoleKey.Escape) return;
                        if (key.Key == ConsoleKey.Enter) break;
                    }
                    contador = 0;
                }
            }



        }
    }
}
