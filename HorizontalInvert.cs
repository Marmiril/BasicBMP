using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Intermedios.BasicBMP
{

    /// <summary>
    /// Flip horizontal (invertir izquierda ↔ derecha)
    /// Aquí no se intercambian filas, sino píxeles dentro de cada fila.
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
    internal class HorizontalInvert
    {
        public void Run()
        {
            string filePath = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image3.bmp";
            string outPath  = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image_flipH.bmp";


            try
            {
                byte[] bmp = File.ReadAllBytes(filePath);

                if (bmp[0] != 'B' || bmp[1] != 'M') throw new InvalidDataException("No se trata de un archivo BMP");

                int dataOffset = BitConverter.ToInt32(bmp, 10);
                int width      = BitConverter.ToInt32(bmp, 18);
                int height     = BitConverter.ToInt32(bmp, 22);
                short bpp      = BitConverter.ToInt16(bmp, 28);

                if (bpp != 24) throw new InvalidDataException("Sólo BMP de 24 bpp.");

                int absHeight = Math.Abs(height);
                int bytesPerPx = 3;
                int rowSize = ((width * bytesPerPx + 3) / 4) * 4;
                /*
                   Un BMP guarda los píxeles fila por fila.
                   Cada píxel ocupa bytesPerPx (en un BMP de 24 bpp son 3 bytes: B, G, R).
                   Pero cada fila debe acabar en un múltiplo de 4 bytes. Si sobra, se añaden bytes de relleno (padding).
                   rowSize es el tamaño total de cada fila de la imagen en bytes, incluyendo ese padding.
                */

                // Reordenar columnas de cada fila.
                for (int y = 0; y < absHeight; y++)
                {
                    int rowStart = dataOffset + y * rowSize;
                    for (int x = 0; x < width / 2; x++)
                    {
                        int left  = rowStart + x * bytesPerPx;
                        int right = rowStart + (width - 1 - x) * bytesPerPx;

                        for (int k = 0; k < bytesPerPx; k++)
                        {
                            byte tmp = bmp[left + k];
                            bmp[left + k] = bmp[right + k];
                            bmp[right + k] = tmp;
                        }
                    }                   
                }
                File.WriteAllBytes(outPath, bmp);
                Console.WriteLine($"Volteo horizontal guardado en:\n {outPath}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }
        }
    }
}
