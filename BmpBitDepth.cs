using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Intermedios.BasicBMP
{

    /// <summary>
    /// Leer los bytes 28–29 del BMP (little-endian) y mostrar BitsPerPixel (1, 4, 8, 16, 24, 32…).
    /// </summary>

    /*
                   El encabezado de una imagen BMP es el siguiente:
                    [0..13]   BITMAPFILEHEADER (14 bytes)
                              0-1   'B' 'M'
                              2-5   Tamaño total del archivo (bfSize)
                              6-9   Reservado (2+2 bytes)
                              10-13 Offset a los datos de píxeles (bfOffBits)

                    [14..(14+biSize-1)] DIB header (normalmente BITMAPINFOHEADER, biSize=40)
                              14-17  Tamaño del DIB (biSize) → suele ser 40
                              18-21  Ancho (biWidth, Int32, little-endian)
                              22-25  Alto  (biHeight, Int32, little-endian; si es negativo = top-down)
                              26-27  Planos (biPlanes, debe ser 1)
                              28-29  Bits por píxel (biBitCount: 1,4,8,16,24,32…)
                              30-33  Compresión (biCompression)
                              34-37  Tamaño de imagen (biSizeImage)
                              38-41  Resol. horizontal (biXPelsPerMeter)
                              42-45  Resol. vertical   (biYPelsPerMeter)
                              46-49  Colores en paleta (biClrUsed)
                              50-53  Colores importantes (biClrImportant)

                    [... ]    (Opcional) tabla de color / paleta (si biBitCount ≤ 8)
                    [bfOffBits..]   Datos de píxeles
    */
    internal class BmpBitDepth
    {
        public void Run()
        {
            string filePath = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image.bmp";

            try
            {
                byte[] header = new byte[30];

                using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                int read = fs.Read(header, 0, header.Length);

                if (read < header.Length)
                {
                    throw new InvalidDataException("El archivo es demasiado corto.");
                }

                if (header[0] != (byte)'B' || header[1] != (byte)'M')
                {
                    throw new InvalidDataException("El archivo no tiene firma BMP.");
                }

                short bitsPerPixel = BitConverter.ToInt16(header, 28);

                Console.WriteLine($"Bits por pixel: {bitsPerPixel}");

                string nota = bitsPerPixel switch
                {
                    1 => "1 bpp: monocromo con paleta (2 colores).",
                    4 => "4 bpp: 16 colores",
                    8 => "8 bpp: 256 colores",
                    16 => "16 bpp: alta color (5-6-5 o 5-5-5-1).",
                    24 => "24 bpp: RGB sin alfa (3 bytes por píxel).",
                    32 => "32 bpp: RGBA o XRGB (4 bytes por píxel).",
                    _ => "Valor no típico o no estándar."
                };

                Console.WriteLine(nota);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error inesperado: {e.Message}");
            }
        }
    }
}
