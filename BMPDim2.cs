using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Intermedios.BasicBMP
{

    /// <summary>
    /// Abrir un archivo BMP y mostrar su ancho y alto en píxeles.
    /// Estos valores están en el encabezado en los bytes 18–21 (ancho) y 22–25 (alto),
    /// en formato little-endian (primero el byte menos significativo).
    /// </summary>
    internal class BMPDim2
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
                using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                byte[] header = new byte[26];

                int read = fs.Read(header, 0, header.Length);

                if (read < header.Length)
                {
                    throw new InvalidDataException("Archivo demasiado pequeño para ser un BMP."); return;
                }

                // Comprobar la firma 
                if (header[0] != (byte)'B' || header[1] != (byte)'M')
                {
                    throw new InvalidDataException("El archivo no tiene firma BMP.");
                }

                // Extraer ancho y alto.
                int width = BitConverter.ToInt32(header, 18);
                int height = BitConverter.ToInt32(header, 22);

                Console.WriteLine($"Dimensiones: {width}x{height}");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error, no se encontró el archivo: {ex.Message}");
            }
            catch (InvalidDataException ex)
            {
                Console.WriteLine($"Error de formato: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Error de E/S: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo: {ex.Message}");
            }

        }
    }
}
