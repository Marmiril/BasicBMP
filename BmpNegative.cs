using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Intermedios.BasicBMP
{

    /// <summary>
    /// Verifica BMP (“BM”), lee bfOffBits, width, height, bitsPerPixel, compression.
    /// Recorre los píxeles(B, G, R) y convierte cada canal a 255 - canal.
    /// Respeta el padding por fila(múltiplo de 4 bytes).
    /// Guarda como image_negativo.bmp.
    /// </summary>
    

    internal class BmpNegative
    {
        public void Run()
        {
            string inputPath  = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image.bmp";
            string outputPath = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image_negativo.bmp";

            try
            {
                byte[] fileBytes = File.ReadAllBytes(inputPath);

                // Las comprobaciones mínimas y clásicas.
                if (fileBytes.Length < 54)
                {
                    throw new InvalidDataException("Archivo demasiado corto para ser BMP.");
                }
                if (fileBytes[0] != (byte)'B' || fileBytes[1] != (byte)'M')
                {
                    throw new InvalidDataException("El archivo no tiene firma BMP");
                }

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

                // Campos necesarios.
                int fileSize = BitConverter.ToInt32(fileBytes, 2);
                int dataOffset = BitConverter.ToInt32(fileBytes, 10);
                int dibSize = BitConverter.ToInt32(fileBytes, 14);
                int width = BitConverter.ToInt32(fileBytes, 18);
                int height = BitConverter.ToInt32(fileBytes, 22); // puede ser negativo (top-down)
                short bpp = BitConverter.ToInt16(fileBytes, 28);
                int compression = BitConverter.ToInt32(fileBytes, 30);

                if (fileSize != fileBytes.Length)
                {
                    // Avisa si el tamaño declarado no coincide con el del archivo.
                    Console.WriteLine("Aviso: el tamaño del archivo no coincide con el declarado.");
                }

                if (dibSize < 40)
                {
                    throw new InvalidDataException("DIB header no soportado (biSize < 40).");
                }

                if(bpp != 8)
                {
                    throw new InvalidDataException($"Sólo se soporta 24 bpp para este ejercicio. Detectado {bpp} bpp.");
                }

                if(compression != 0)
                {
                    throw new InvalidDataException("Sólo se soportan BMP sin compresión (biCompression = 0).");
                }

                // Cálculo de stride (bytes por fila con paddin a múltiplo de 4.
                int absHeight = Math.Abs(height);
                int bytesPerPixel = 3;
                int rowSize = ((width * bytesPerPixel + 3) / 4) * 4; // Alineado a 4.

                // Validáción básica de rangos.
                if (dataOffset + (long)rowSize * absHeight > fileBytes.LongLength)
                {
                    throw new InvalidDataException("Los datos de imagen no caben en el archivo. Encabezado inconsistente.");
                }

                // Recorrido de píxeles.
                // BMP bottom-up si heigth > 0; top-down si < 0.

                bool bottomUp = height > 0;

                for (int y = 0; y < absHeight; y++)
                {
                    int scrRow = bottomUp ? (absHeight - 1 - y) : y; //Índice lógico de lectura/escritura.
                    int rowStart = dataOffset + scrRow * rowSize;

                    for ( int x = 0; x < width; x++)
                    {
                        int px = rowStart + x * bytesPerPixel;

                        // Orden BMP: B, G, R.
                        fileBytes[px + 0] = (byte)(255 - fileBytes[px + 0]); // B
                        fileBytes[px + 1] = (byte)(255 - fileBytes[px + 1]); // G
                        fileBytes[px + 2] = (byte)(255 - fileBytes[px + 2]); // R                        
                    }
                }

                // Guardar resultado.
                File.WriteAllBytes(outputPath, fileBytes);
                Console.WriteLine($"Negativo guardado en \n {outputPath}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}");
            }

        }
    }
}
