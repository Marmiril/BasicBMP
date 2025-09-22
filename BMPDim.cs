using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace Intermedios.BasicBMP
{

    /// <summary>
    /// Programa en C# para leer las dimensiones de una imagen con formato Windows bitmap. 
    /// Primero comprobar que se trata de una imagen .bmp válida, revisando los datos de cabecera 'BM'.
    /// Si se trata de una imagen .bmp válida entonces obtiene sus dimensiones (ancho x alto) ylas muestra en pantalla.
    /// Es un formato propio del sistema operativo Windows.Puede guardar imágenes hasta 24 bits (16,7 millones de colores).
    /*
        El encabezado de una imagen BMP es el siguiente:

        Descripción .........................Bytes
        Tipo(BM)...............................0-1
        Tamaño	...............................2-5
        Reservado	...........................6-9
        Inicio de los datos de la imagen	.10-13
        Tamaño del bitmap	.................14-17
        Ancho(píxeles) ......................18-21
        Alto(píxeles)  ......................22-25
        Número de planos	.................26-27
        Tamaño de cada punto   ............. 28-29
        Compresión	.........................30-33
        Tamaño de imagen	.................34-37
        Resolución horizontal  ............. 38-41
        Resolución vertical .................42-45
        Tamaño de la tabla de color .........46-49
        Contador de colores	.................50-53
    */
    /// </summary>
    internal class BMPDim
    {
        public void Run()
        {
            string filePath = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image.bmp";

            int size = 54, width, height;

            byte[] data = new byte[size];

            using (FileStream fs = File.OpenRead(filePath))
            {
                fs.Read(data, 0, size);
            }

            if (data[0] != 'B' || data[1] != 'M')
            {
                Console.WriteLine("No se trata de un archivo BMP."); return;
            }

            width  = data[18] + (data[19] * 256) +
                     (data[20] * 256 * 256)      +
                     (data[21] * 256 * 256 * 256);

            height = data[22] + (data[23] * 256) +
                     (data[24] * 256 * 256)      +
                     (data[25] * 256 * 256 * 256);

            Console.WriteLine($"{width}x{height}, ancho, alto.");
        }
    }
}
