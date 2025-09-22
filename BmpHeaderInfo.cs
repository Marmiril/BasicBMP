using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Intermedios.BasicBMP
{

    /// <summary>
    /// Calcular el tamaño en bytes de la cabecera y de los datos
    /// El tamaño total del archivo está en los bytes 2–5.
    /// El offset(dónde empiezan los datos de imagen) está en los bytes 10–13.
    /// De ahí sacamos:
    /// Tamaño cabecera = offset de datos.
    /// Tamaño datos = tamaño total – offset.
    /// </summary>        
    /*
    El encabezado de una imagen BMP es el siguiente:

        Descripción.........................Bytes
        Tipo(BM)..............................0-1
        Tamaño................................2-5
        Reservado.............................6-9
        Inicio de los datos de la imagen....10-13
        Tamaño del bitmap...................14-17
        Ancho(píxeles) .....................18-21
        Alto(píxeles)  .....................22-25
        Número de planos....................26-27
        Tamaño de cada punto................28-29
        Compresión..........................30-33
        Tamaño de imagen....................34-37
        Resolución horizontal ............. 38-41
        Resolución vertical.................42-45
        Tamaño de la tabla de color.........46-49
        Contador de colores.................50-53
    */
    internal class BmpHeaderInfo
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
                byte[] header = new byte[14];

                using var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                int read = fs.Read(header, 0, header.Length);

                if (read < header.Length)
                {
                    throw new InvalidDataException("El archivo es demasiado corto para ser BMP");
                }

                if (header[0] != (byte)'B' || header[1] != (byte)'M')
                {
                    throw new InvalidDataException("No tiene firma BMP, no es BMP");
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
                // Tamaño total del archivo (bytes 2 - 5).
                int fileSize = BitConverter.ToInt32(header, 2);

                // Offset al inicio de los datos de imagen (bytes 10 - 13).
                int dataOffset = BitConverter.ToInt32(header, 10);

                int headerSize = dataOffset;
                int imageSize = fileSize - dataOffset;

                Console.WriteLine($"Tamaño total: {fileSize} bytes");
                Console.WriteLine($"Tamaño de cabecera: {headerSize} bytes");
                Console.WriteLine($"Tamaño de la imagen: {imageSize} bytes");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo: {ex.Message}");
            }
        }
    }
}
