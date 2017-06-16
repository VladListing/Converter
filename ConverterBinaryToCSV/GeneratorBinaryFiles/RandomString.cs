using System;

namespace GeneratorBinaryFiles
{
    //класс 'RandomString' заполняем столбец 'коментарий' случайными значениями

    class RandomString
    {
        Random r = new Random();

        private string sumString = "";// "склеянная строка со случайными значениями по текущей сделке"
        private int number = 0; //номер варианта случайной строки
        private int profit = 0; //прибыль
        private int loss = 0;   //убыток
        public string GetCommentRandom()
        {
            number = r.Next(0, 6);
            loss = r.Next(-1000, 0);
            profit = r.Next(0, 10000);
                        
            switch (number)
            {
                case 0: sumString = "   trade:Sell   result:Profit     " + "   +" + profit + " $"; break;
                case 1: sumString = "   trade:Sell   result:Loss           " + loss + " $"; break;
                case 2: sumString = "   trade:Bay    result:Profit     " + "  +" + profit + " $"; break;
                case 3: sumString = "   trade:Bay    result:Loss          " + loss + " $"; break;
                case 4: sumString = "   trade:Bay    result:Stoploss  " + loss + " $"; break;
                case 5: sumString = "   trade:Sell   result:Stoploss  " + loss + " $"; break;
            }

            return sumString;
        }
   }
}
