using System;
using System.IO;
using System.Runtime.InteropServices;
using BinaryFilesConvertor;

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
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)] public string comment;

            public TradeRecord(int a, int b, double c, string d)
            {
                id = a;
                account = b;
                volume = c;
                comment = d;
            }
        }

        private string pathBinaryFiles = ""; //путь и имя создаваемого бинарного файла.
        private long counter = 0; //счетчик записаных в файл строк.        

        public void BinaryFilesGener(string pathBinaryFiles, long quantityLine)
        {
            //
            this.pathBinaryFiles = pathBinaryFiles;

            Console.SetWindowSize(90, 20);

            //todo: why so many WriteLines? We can use \n. And btw you may use WriteLine without any arguments.
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Ждите, выполняется запись в  файл: {0} ", pathBinaryFiles);
            Console.WriteLine("");

            try
            {
                //todo: тут иемеет смысл передать только один раз в конструктор quantityLine
                ProcessMappingBase processMapping = new ProcessGenerationMapping(quantityLine);

                //todo: это тут не надо, как мы можем обойтись без этой операции
                File.WriteAllText(pathBinaryFiles, ""); //очистка содержимого файла.
                
                using (var writer = new BinaryWriter(File.Open(pathBinaryFiles, FileMode.Append, FileAccess.Write)))
                {
                    for (int i = 0; i < quantityLine; i++)
                    {
                        //todo: имена переменных
                        TradeRecord trades = new TradeRecord(0 + i, 777, 640 + i, RandomString.GetCommentRandom());
                        //todo: зачем тут блок?
                        {
                            writer.Write(trades.id);
                            writer.Write(trades.account);
                            writer.Write(trades.volume);
                            writer.Write(trades.comment);

                            counter++;
                        }
                        //отображение текушего процента выполнения    
                        processMapping.ProcessMappingInPercent();
                    }
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
