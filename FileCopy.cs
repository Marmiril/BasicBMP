using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermedios.BasicBMP
{
    /// <summary>
    /// Programa en C# que realiza copias de archivos tanto de texto como binarios.
    /// Utiliza FileStream asignando un tamaño de buffer de 512 Kb.
    /// </summary>
    internal class FileCopy
    {
        public void Run()
        {
            const int bufferSize = 512 * 1024;

            byte[] data = new byte[bufferSize];

            try
            {
                string fileIn = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image.bmp";
                string fileOut = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image_copy.bmp";

                using (FileStream fileEnter = File.OpenRead(fileIn))
                {
                    using (FileStream fileExit = File.Create(fileOut))
                    {
                        int totalRead;

                        do
                        {
                            totalRead = fileEnter.Read(data, 0, bufferSize);
                            fileExit.Write(data, 0, totalRead);
                        }
                        while (totalRead == bufferSize);

                        Console.WriteLine($"Copia creada en: {fileOut}.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado: {ex.Message}.");
            }
           
        }
    }
}
