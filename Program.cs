using System;
using System.Diagnostics;
using System.IO;

namespace Recursividad
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rutaInicial = Path.Combine("C:", "Carpetas");

            int totalArchivos = 0;
            int totalCarpetas = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            ExplorarCarpetasIterativo(rutaInicial, ref totalArchivos, ref totalCarpetas);
            stopwatch.Stop();
            Console.WriteLine($"Tiempo solución iterativa: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Total de archivos: {totalArchivos}");
            Console.WriteLine($"Total de carpetas: {totalCarpetas}");

            totalArchivos = 0; // Reiniciar contadores
            totalCarpetas = 0;

            stopwatch.Reset();
            stopwatch.Start();
            ExplorarCarpetasRecursivo(rutaInicial, ref totalArchivos, ref totalCarpetas);
            stopwatch.Stop();
            Console.WriteLine($"Tiempo solución recursiva: {stopwatch.ElapsedMilliseconds} ms");
            Console.WriteLine($"Total de archivos: {totalArchivos}");
            Console.WriteLine($"Total de carpetas: {totalCarpetas}");
        }

        static void ExplorarCarpetasRecursivo(string ruta, ref int totalArchivos, ref int totalCarpetas)
        {
            try
            {
                // Contar archivos en la ruta actual
                string[] archivos = Directory.GetFiles(ruta);
                totalArchivos += archivos.Length;

                // Contar carpetas en la ruta actual
                string[] carpetas = Directory.GetDirectories(ruta);
                totalCarpetas += carpetas.Length;

                // Procesar archivos
                foreach (var archivo in archivos)
                {
                    Console.WriteLine($"Archivo: {archivo}");
                }

                // Procesar carpetas recursivamente
                foreach (var carpeta in carpetas)
                {
                    Console.WriteLine($"Carpeta: {carpeta}");
                    ExplorarCarpetasRecursivo(carpeta, ref totalArchivos, ref totalCarpetas);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Acceso denegado a la carpeta : {ruta}");
            }
        }

        static void ExplorarCarpetasIterativo(string ruta, ref int totalArchivos, ref int totalCarpetas)
        {
            try
            {
                // Usar una pila para manejar la exploración iterativa
                Stack<string> stack = new Stack<string>();
                stack.Push(ruta);

                while (stack.Count > 0)
                {
                    string directorioActual = stack.Pop();

                    // Contar archivos en la ruta actual
                    string[] archivos = Directory.GetFiles(directorioActual);
                    totalArchivos += archivos.Length;

                    // Procesar archivos
                    foreach (var archivo in archivos)
                    {
                        Console.WriteLine($"Archivo: {archivo}");
                    }

                    // Contar carpetas en la ruta actual
                    string[] carpetas = Directory.GetDirectories(directorioActual);
                    totalCarpetas += carpetas.Length;

                    // Agregar carpetas a la pila para procesarlas
                    foreach (var carpeta in carpetas)
                    {
                        Console.WriteLine($"Carpeta: {carpeta}");
                        stack.Push(carpeta);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Acceso denegado a la carpeta : {ruta}");
            }
        }
    }
}