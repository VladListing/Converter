using System;
using System.IO;
using System.Text;
using System.Threading;
using ConvertingBinaryToCsv_Library;



namespace ConvertingBinaryToCsvLibrary
{
    //библиотека 'ConvertBinaryToCsvLibrary' реализует 
    //чтение из бинарного файла с последующей конвертацией в CSV файл.

    public class ConvertBinaryToCsvLibrary : IBinaryToCsv
    {
        private string pathBinary;   //путь и имя исходного бинарного файла
        private string pathCsv;      //путь и имя конечного Csv файла

        private int id = 0;          //колонка "id"
        private int account = 0;     //колонка "account"
        private double volume = 0.0; //колонка "volume"
        private string comment = ""; //колонка "comment"
        
        private int counter = 0;     //счетчик записаных строк
        
        public void FromBinaryFileToCsv(string pathDat, string pathCsv)
        {
            this.pathBinary = pathDat;
            this.pathCsv = pathCsv;
            
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
                ProcessMapping processMapping = new ProcessMapping();
                long initialValue = new FileInfo(pathDat).Length;
                using (BinaryReader reader = new BinaryReader(File.Open(this.pathBinary, FileMode.Open), Encoding.ASCII))
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(this.pathCsv))
                {
                    Console.WriteLine("Ждите, выполняется конвертация из бинарного файла: {0} , размером: {1} Mb", this.pathBinary, initialValue/1048576);
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
                        processMapping.processMappingInPercent( initialValue, pathCsv, 1.1 , true);                    
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


