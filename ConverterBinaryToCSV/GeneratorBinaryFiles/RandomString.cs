using System;

namespace GeneratorBinaryFiles
{
    //класс 'RandomString' заполняем столбец 'коментарий' случайными значениями

    class RandomString
    {
        public static string GetCommentRandom()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            //todo: тут поля как таковые не имеют смысла вообще. Проще всё сделать в теле метода, а сам метод статичным
            var number = r.Next(0, 6);
            var loss = r.Next(-1000, 0);
            var profit = r.Next(0, 10000);
            string sumString = null;
            

            switch (number)
            {
                case 0:
                    sumString = $"   trade:Sell   result:Profit        +{profit} $";
                    break;
                case 1:
                    sumString = "   trade:Sell   result:Loss           " + loss + " $";
                    break;
                case 2:
                    sumString = "   trade:Bay    result:Profit     " + "  +" + profit + " $";
                    break;
                case 3:
                    sumString = "   trade:Bay    result:Loss          " + loss + " $";
                    break;
                case 4:
                    sumString = "   trade:Bay    result:Stoploss  " + loss + " $";
                    break;
                case 5:
                    sumString = "   trade:Sell   result:Stoploss  " + loss + " $";
                    break;
            }

            return sumString;
        }
   }
}
