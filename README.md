Descripción
Este repositorio contiene una colección de ejercicios en C# relacionados con la lectura, escritura y manipulación de archivos binarios, principalmente en formato BMP.
Cada clase implementa un programa de consola independiente que ilustra conceptos básicos de acceso a bajo nivel en archivos.
Ejercicios incluidos

* Visores hexadecimales

* VisorHexadecimal, HexVisorPart, HexVisorv2, HexVisorv3: muestran el contenido de un archivo en hexadecimal y ASCII, con paginación de 24 filas.

* Lectura de cabeceras BMP

* BmpCheck: comprueba la firma “BM”.

* BMPDim, BMPDim2: muestran ancho y alto de la imagen.

* BmpBitDepth: lee la profundidad de color (bits por píxel).

* BmpHeaderInfo: calcula tamaño de cabecera y datos.

* BmpHeaderDump: muestra los primeros 16 bytes en hexadecimal y ASCII.

* Manipulación de imágenes BMP

* HorizontalInvert: voltea horizontalmente la imagen.

* ImageInverter: voltea verticalmente la imagen.

* BmpNegative: genera el negativo de una imagen BMP.

* Operaciones con archivos

* FileCopy: copia un archivo con buffer de 512 KB.

* FileInverter: invierte un archivo byte a byte.

* SplitFile: divide un archivo en partes de 5 KB.

* Alteración de la firma BMP

* CipherBMP: cambia la cabecera de “BM” a “MB”.

* ToggleCipher: alterna entre “BM” y “MB”.

* FixCipher: restaura la firma original “BM”.

  Ejecución

Cada clase tiene un método Run() que puede llamarse desde Program.cs o ejecutarse como proyecto de consola en Visual Studio.
Es necesario ajustar las rutas de archivo a las carpetas locales antes de probar.



Description

This repository contains a set of C# exercises focused on reading, writing and manipulating binary files, mainly in BMP format.
Each class is an independent console program demonstrating low-level file access.

Included exercises

* Hex viewers

* VisorHexadecimal, HexVisorPart, HexVisorv2, HexVisorv3: display a file’s contents in hexadecimal and ASCII, with 24-line pagination.

* BMP header reading

* BmpCheck: verifies the “BM” signature.

* BMPDim, BMPDim2: print image width and height.

* BmpBitDepth: reads bits per pixel.

* BmpHeaderInfo: calculates header size and data size.

* BmpHeaderDump: prints the first 16 bytes in hex and ASCII.

* BMP image manipulation

* HorizontalInvert: horizontal flip.

* ImageInverter: vertical flip.

* BmpNegative: generates a negative of the BMP image.

* File operations

* FileCopy: copy a file with a 512 KB buffer.

* FileInverter: invert file contents byte by byte.

* SplitFile: split a file into 5 KB parts.

* BMP signature alteration

* CipherBMP: changes header from “BM” to “MB”.

* ToggleCipher: toggles between “BM” and “MB”.

* FixCipher: restores the original “BM” signature.

Execution

Each class has a Run() method that can be called from Program.cs or executed as a console project in Visual Studio.
File paths should be adjusted to match local folders before running.
