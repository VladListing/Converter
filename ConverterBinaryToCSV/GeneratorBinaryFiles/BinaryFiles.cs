using System;
using System.IO;
using System.Runtime.InteropServices;

namespace GeneratorBinaryFiles

// класс 'BinaryFiles' генерирует бинарные файлы с заданым количеством строк.
{
    class BinaryFiles
    {
        //описание структуры 'TradeRecord'.
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct TradeRecord
        {
            public int id;
            public int account;
            public double volume;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string comment;

            public TradeRecord(int a, int b, double c, string d)
            {
                id = a;
                account = b;
                volume = c;
                comment = d;
            }
        }
        
        private string pathBinaryFiles = ""; //путь и имя создаваемого бинарного файла.
        private long quantityLine = 0;       //количество строк в создаваемом бинарном файле.
        private long counter = 0;            //счетчик записаных в файл строк.        
                
        public void BinaryFilesGener(string pathBinaryFiles, long quantityLine )
        {
            this.pathBinaryFiles = pathBinaryFiles;
            this.quantityLine = quantityLine;

            Console.SetWindowSize(90, 20);
            
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Ждите, выполняется запись в  файл: {0} ", pathBinaryFiles);
            Console.WriteLine("");

            RandomString randoomString = new RandomString();
            
        try
          {
                ProcessMapping processMapping = new ProcessMapping();

                File.WriteAllText(pathBinaryFiles, "");//очистка содержимого файла.
                
                using (BinaryWriter writer = new BinaryWriter(File.Open(pathBinaryFiles, FileMode.Append, FileAccess.Write)))
                                 
                       
            for (int i = 0; i<quantityLine; i++)
            {
                TradeRecord trades = new TradeRecord(0 + i, 777, 640 + i, randoomString.GetCommentRandom());                  
                {
                    writer.Write(trades.id);
                    writer.Write(trades.account);
                    writer.Write(trades.volume);
                    writer.Write(trades.comment);

                    counter++;                     
                }
                //отображение текушего процента выполнения    
                processMapping.processMappingInPercent(this.quantityLine, pathBinaryFiles, 1, false);                              
            }

            Console.Write("\r");
            Console.Write("выполнено: 100 % ");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("в файл {0} записано  {1}  строк(и) ", pathBinaryFiles, counter);
                      
          }            
          catch (Exception m)
          {
                Console.WriteLine(m.Message);
          }
                Console.ReadLine();            
       }
   }   
}
