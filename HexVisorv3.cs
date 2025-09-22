using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Intermedios.BasicBMP
{
    internal class HexVisorv3
    {

        public void Run()
        {
            string filePath = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image.bmp";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("El archivo no existe."); return;
            }

            try
            {
                using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                const int bytes_row = 16;
                const int rows_page = 24;


                byte[] row = new byte[bytes_row];

                while (true)
                {
                    int rowsPrinted = 0;

                    while(rowsPrinted < rows_page)
                    {
                        // Aquí se imprime el offset.
                        Console.Write($"{fs.Position:x8}  ");

                        int read = fs.Read(row, 0, bytes_row);

                        if (read == 0)
                        {
                            Console.WriteLine(); // línea vacía si EOF al entrar.
                            break;
                        }

                        for (int i = 0; i < read; i++)
                        {
                            if (i > 0) Console.Write(' ');
                            Console.Write(row[i].ToString("x2"));
                        }

                        // Si faltan bytes para llegar a 16 se añaden espacios de relleno.
                        // Cada byte ocupa "2 chars + 1 espacio" salvo el primero.
                        int hexPrintedChars = (read == 0) ? 0 : (read * 3 - 1);
                        int hexFullLineChars = 16 * 3 - 1; // 47.

                        if (hexPrintedChars < hexFullLineChars)
                        {
                            Console.Write(new string(' ', hexFullLineChars - hexPrintedChars));
                        }

                        // Separador entre HEX y ASCII.
                        Console.Write("  ");

                        // ASCII: bytes < 32 -> '.', el resto tal cual.
                        var sbAscii = new StringBuilder();

                        for (int i = 0; i < read; i++)
                        {
                            byte b = row[i];
                            char c = (b >= 32 && b <= 126) ? (char)b : '.';
                            sbAscii.Append(c);
                        }

                        Console.WriteLine(sbAscii.ToString());
                        rowsPrinted++;

                        if (read < bytes_row) break; // Última línea parcial ->EOF. 
                    }

                    if (fs.Position >= fs.Length) break;

                    Console.WriteLine("\n- Pulse ENTER para ver más (ESC para salir).");

                    while (true)
                    {
                        var key = Console.ReadKey(intercept: true);

                        if (key.Key == ConsoleKey.Escape) return;

                        if (key.Key == ConsoleKey.Enter) break;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }

        }


    }
}
