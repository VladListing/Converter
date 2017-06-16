using System;
using System.Threading;
using ConvertingBinaryToCsvLibrary; //подключение конвертирующей библиотеки 

namespace Converting
{
    class Program
    {
        static void Main(string[] args)
        {
            //путь и имя бинарного файла со структурами, присвоение из файла настроек: 'Path.settings'
            string pathBinary = Path.Default.pathBinary;

            //путь и имя создаваемого файла с разделителями, типа *.CSV , присвоение из файла настроек: 'Path.settings'
            string pathCsv = Path.Default.pathCsv; 
            
            Console.SetWindowSize(90, 20);

            try
            {
                ConvertBinaryToCsvLibrary convertBinaryToCsvLibrary = new ConvertBinaryToCsvLibrary();

                //создаем фоновый поток
                ThreadStart writeSecond = new ThreadStart(delegate() { convertBinaryToCsvLibrary.FromBinaryFileToCsv(pathBinary, pathCsv); });
                Thread thread = new Thread(writeSecond);                
                thread.IsBackground = true; 
                thread.Start();
                thread.Join(); 
               
                Console.WriteLine(new string('_', 29));
                Console.WriteLine("Статус фонового потока: {0}", thread.ThreadState);
            }
            catch (Exception m)
            {
                Console.WriteLine(m.Message);
            }  

            Console.ReadLine();
        }
    }
}
