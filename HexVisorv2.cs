using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermedios.BasicBMP
{
    internal class HexVisorv2
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

                    while (rowsPrinted < rows_page)
                    {
                        int read = fs.Read(row, 0, bytes_row);

                        if (read == 0) break;

                        // Hex - dos dígitos por byte, separados por espacio.
                        var sbHex = new StringBuilder();

                        for (int i = 0; i < read; i++)
                        {
                            if (i > 0) sbHex.Append(' ');
                            
                            sbHex.Append(row[i].ToString("x2"));
                        }

                        // Alineación de ancho de 16 bytes -> 16 * 3 - 1 = 47.
                        while (sbHex.Length < 47) sbHex.Append(' ');

                        // Substitución de < 32 por '.'.
                        var sbAscii = new StringBuilder();
                        for (int i = 0; i < read; i++)
                        {
                            byte b = row[i];
                            char c = (b >= 32 && b <= 126) ? (char)b : '.';
                            sbAscii.Append(c);
                        }

                        Console.WriteLine($"{sbHex}  {sbAscii}");
                        rowsPrinted++;

                        if (read < bytes_row) break;  // Última línea parcial -> EOF.
                    }

                    if (fs.Position >= fs.Length) break; // No hay más datos.

                    Console.WriteLine("\n- Pulse ENTER para ver más (ESC para salir).");
                    var key = Console.ReadKey(intercept: true);

                    if (key.Key == ConsoleKey.Escape) break;
                }




            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }

        }
    }
}
