using System;

//класс 'InputValidation' проверяет правильность ввода с клавиатуры целых положительных чисел

namespace GeneratorBinaryFiles
{
    class InputValidation
    {
        private int counter = 0;        //счетчик введеных символов
        private int charNum = 0;        //номер по таблице символов
        private string stringsKey = ""; //введенное число (предварительно)
        private long checkedNumber;     //проверенное введеное число

        //from symbols table
        private const char ZeroChar = '0';
        private const char NineChar = (char)57;

        public long GetInputValidationKey()
        {
            Console.WriteLine("");
            Console.WriteLine("Введите количество строк в создаваемом бинарном файле :");
            Console.WriteLine("");

            ConsoleKeyInfo any = new ConsoleKeyInfo();

        a: while (any.Key != ConsoleKey.Enter)
            {
                if (Console.KeyAvailable == true)
                {                    
                    charNum = (int)Console.ReadKey().KeyChar;
                    if (charNum >= ZeroChar && charNum <= NineChar)
                    {
                        stringsKey += (char)charNum;
                        counter++;
                    }
                    else
                    {
                        counter = 0;
                        stringsKey = "";
                        Console.Write("\r");
                        Console.Write("Ошибка, введите целое положительное число:");
                        Console.WriteLine();
                        goto a;
                    }
                    any = Console.ReadKey(true);
                }            
          }
            if (counter != 0 && any.Key == ConsoleKey.Enter)
            {
                checkedNumber = Convert.ToInt32(stringsKey);
            }
            return checkedNumber;
        }

        public static uint GetCountFromUser()
        {
            Console.WriteLine("Enter entries count:");
            while(true)
            {
                var inputStr = Console.ReadLine();
                uint result;
                if (uint.TryParse(inputStr, out result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine($"Incorrect input format: '{inputStr}'. Please try again using only integer positive numbers:");
                }
            }
        }

    }
}
