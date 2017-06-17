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

            bool revert = true;
            
            Console.SetWindowSize(90, 20);

            try
            {
                IConverter converter = GetConverter(revert);
                //converter.Convert(pathBinary, pathCsv);
                var task  = converter.ConvertAsync(pathBinary, pathCsv);
                task.Wait();

                ConvertBinaryToCsvLibrary convertBinaryToCsvLibrary = new ConvertBinaryToCsvLibrary();

                //создаем фоновый поток
                ThreadStart writeSecond = new ThreadStart(delegate()
                {
                    convertBinaryToCsvLibrary.FromBinaryFileToCsv(pathBinary, pathCsv);
                });
                Thread thread = new Thread(writeSecond);                
                thread.IsBackground = true; 
                thread.Start();
                //todo: в этом месте теряется смысл всего нового потока, так как мы всё равно ждём его заврешения
                thread.Join(); 
               
                Console.WriteLine(new string('_', 29));
                Console.WriteLine("Статус фонового потока: {0}", thread.ThreadState);
            }
            // todo: любые ошибки из фонового потока сюда не попадут, и просто уронят программу
            catch (Exception m)
            {
                
                Console.WriteLine(m.Message);
            }  

            Console.ReadLine();
        }

        private static IConverter GetConverter(bool revert)
        {
            if (revert)
            {
                return new CsvToBinaryConverter();
            }
            else
            {
                return new BinaryToCsvConverter();
            }
        }
    }
}
