using System;


namespace GeneratorBinaryFiles
{
    ///<symmary>
    ///класс 'InputValidation' проверяет правильность ввода с клавиатуры целых положительных чисел
    ///<symmary>
    
    class InputValidation
    {
        private int _counter = 0;          //счетчик введеных символов
        private int _charNum = 0;          //номер по таблице символов
        private string _stringsKey = null; //введенное число (предварительно)
        private long _checkedNumber = 0;   //проверенное введеное число

        //из таблици символов Юникода
        private const char _zeroChar = (char) 48;
        private const char _nineChar = (char) 57;

        public long GetInputValidationKey()
        {
            Console.WriteLine();
            Console.WriteLine("Введите количество строк в создаваемом бинарном файле :");
            Console.WriteLine();

            ConsoleKeyInfo any = new ConsoleKeyInfo();

            while (any.Key != ConsoleKey.Enter)
            {
                if (Console.KeyAvailable == true)
                {
                    _charNum = (int)Console.ReadKey().KeyChar;
                    if (_charNum >= 48 && _charNum <= 57)
                    {
                        _stringsKey += (char)_charNum;
                        _counter++;
                    }
                    else
                    {
                        _counter = 0;
                        _stringsKey = null;
                        Console.Write("\r");
                        Console.Write("Ошибка, введите целое положительное число:");
                        Console.WriteLine();
                        continue;
                    }
                    any = Console.ReadKey(true);
                }
            }
            if (_counter != 0 && any.Key == ConsoleKey.Enter)
            {
                _checkedNumber = Convert.ToInt32(_stringsKey);
            }
            return _checkedNumber;
        }
    }
}
