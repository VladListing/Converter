using System;
using System.IO;
using System.Runtime.InteropServices;

namespace GeneratorBinaryFiles
///<symmary>
/// класс 'BinaryFiles' генерирует бинарный файл с заданым количеством строк, заданой структурой.
///<symmary>
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
        
        private string _pathBinaryFiles = ""; //путь и имя создаваемого бинарного файла.
        private long _quantityLine = 0;       //количество строк в создаваемом бинарном файле.
        private long _counter = 0;            //счетчик записаных в файл строк.        
                
        public void BinaryFilesGener(string pathBinaryFiles, long quantityLine )
        {
            this._pathBinaryFiles = pathBinaryFiles;
            this._quantityLine = quantityLine;

            Console.SetWindowSize(90, 20);
            
            Console.WriteLine("\n\n");
            Console.WriteLine("Ждите, выполняется запись в  файл: {0} ", pathBinaryFiles);
            Console.WriteLine();

            RandomString randoomString = new RandomString();
            
          try
          {
                //todo: тут иемеет смысл передать только один раз в конструктор quantityLine
                //ProcessMappingBase processMapping = new ProcessGenerationMapping(quantityLine);

                ProcessMapping processMapping = new ProcessMapping();

                //очистка содержимого файла (на случай перезаписи существующего файла).
                File.WriteAllText(pathBinaryFiles, "");
                
                using (BinaryWriter writer = new BinaryWriter(File.Open(pathBinaryFiles, FileMode.Append, FileAccess.Write)))
                                 
                       
            for (int i = 0; i<quantityLine; i++)
            {
                TradeRecord trades = new TradeRecord(0 + i, 777, 640 + i, randoomString.GetCommentRandom());                  
                {
                    writer.Write(trades.id);
                    writer.Write(trades.account);
                    writer.Write(trades.volume);
                    writer.Write(trades.comment);

                    _counter++;                     
                }
                //отображение текушего процента выполнения    
                processMapping.processMappingInPercent(this._quantityLine, pathBinaryFiles, 1, false);                              
            }

            Console.Write("\r");
            Console.Write("выполнено: 100 % ");
            Console.WriteLine("\n");
            Console.WriteLine("в файл {0} записано  {1}  строк(и) ", pathBinaryFiles, _counter);
                      
          }            
          catch (Exception m)
          {
                Console.WriteLine(m.Message);
          }
                Console.ReadLine();            
       }
   }   
}
