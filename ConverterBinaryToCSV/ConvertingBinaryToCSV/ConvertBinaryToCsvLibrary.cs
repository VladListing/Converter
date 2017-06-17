using System;
using System.IO;
using System.Text;
using System.Threading;
using BinaryFilesConvertor;


namespace ConvertingBinaryToCsvLibrary
{
    //библиотека 'ConvertBinaryToCsvLibrary' реализует 
    //чтение из бинарного файла с последующей конвертацией в CSV файл.

        /// <summary>
        /// 
        /// </summary>
    public class ConvertBinaryToCsvLibrary : IBinaryToCsv
    {
        private string pathBinary;   //путь и имя исходного бинарного файла
        private string pathCsv;      //путь и имя конечного Csv файла

        private int id = 0;          //колонка "id"
        private int account = 0;     //колонка "account"
        private double volume = 0.0; //колонка "volume"
        private string comment = ""; //колонка "comment"
        
        private int counter = 0;     //счетчик записаных строк

        private const double ByteToMegabyteKoef = 1 / 1048576.0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pathDat"></param>
        /// <param name="pathCsv"></param>
        public void FromBinaryFileToCsv(string pathDat, string pathCsv)
        {
            this.pathBinary = pathDat;
            this.pathCsv = pathCsv;
            
            //todo: вот консоль тут уже совершенно ни при чём, а если мы решим использовать библиотеку в оконном приложении
            Console.SetWindowSize(90,20);

            Thread f = Thread.CurrentThread;
            f.Name = "'ConvertingBinaryToCsvLibrary'";
            Console.WriteLine("Имя фонового потока: {0}", f.Name);            
            Console.WriteLine("Статус фонового потока: {0}", f.ThreadState);
            Console.WriteLine("Запущен ли фоновый поток: {0}", f.IsAlive);
            Console.WriteLine(new string('_', 29));
            Console.WriteLine();
            
            try
            {
                ProcessMappingBase processMapping = new ProcessConvertionMapping(pathDat, pathCsv);
                using (BinaryReader reader = new BinaryReader(File.Open(this.pathBinary, FileMode.Open), Encoding.ASCII))
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(this.pathCsv))
                {
                    Console.WriteLine($"Ждите, выполняется конвертация из бинарного файла: {pathBinary} , размером: Mb");
                    Console.WriteLine("в файл с разделителями: {0} ", this.pathCsv);
                    Console.WriteLine();
                    
                    reader.BaseStream.Position = 0;

                    while (reader.PeekChar() > -1)
                    {
                        id = reader.ReadInt32();
                        account = reader.ReadInt32();
                        volume = reader.ReadDouble();
                        comment = reader.ReadString();
                        
                        file.Write(id);
                        file.Write(";");
                        file.Write(account);
                        file.Write(";");
                        file.Write(volume);
                        file.Write(";");
                        file.Write(comment);
                        file.WriteLine(";");
                        counter++;

                        //процент выполнения    
                        processMapping.ProcessMappingInPercent();                    
                    }                    
                        Console.Write("\r");
                        Console.Write("выполнено: 100 % ");
                    
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.Write("в файл {0} конвертировано: {1} строк(и). ", pathCsv , counter);
                    Console.WriteLine();
                    
                    counter = 0;
                }                      
            }
            catch (Exception m)
            {
                Console.WriteLine(m.Message);
            }                       
        }       
    }
}


