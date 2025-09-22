using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intermedios.BasicBMP
{

    /// <summary>
    /// Programa en C# que divide archivos de texto o binarios en partes de 5 Kb cada una.
    /// Utilizar FileStream para leer archivos y escribir las diferentes partes de este.
    /// 
    /// </summary>
    internal class SplitFile
    {
        public void Run()
        {
            try
            {
                int bufferSize = 5 * 1024;
                byte[] data = new byte[bufferSize];

                int totalRead = '\0';
                int counter = 1;

                string fileIn = @"E:\03PROGRAMACIÓN\C#\Intermedios\Intermedios\BasicBMP\image.bmp";
              //  string ext     = ".exe";

                using (FileStream inputFile = File.OpenRead(fileIn))
                {
                    while ((totalRead = inputFile.Read(data, 0, bufferSize)) > 0) 
                    {
                        string partName = fileIn + $".part{counter:00}";


                            using (FileStream newFile = File.Create(partName))
                            {
                                newFile.Write(data, 0, totalRead);
                                
                            }
                        counter++;
                    }
                    
                }
                Console.WriteLine("Archivo dividido en partes correctamente.");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
