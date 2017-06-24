using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConvertingBinaryToCSV
{
    //класс 'processMapping' отображение процента выполнения 
    //текущего процесса конвертации (загрузки) в процентах.

    class ProcessMapping
    {
        private double processPercent = 0; //процент выполнения процесса.
        private int displayPeriod = 0;     //периодичность отображения.
        private int counter1 = 0;          //счетчик проходов.
        private long counter = 0;          //счетчик проходов.
        private long finalValue = 0;       //текущий размер конечного файла.

        public void processMappingInPercent(long initialValue, string pathFinalValue, double correctionValue, bool mode)
        {
            counter = counter + 1;
            counter1 = counter1 + 1;

            if (initialValue < 1000)
            { displayPeriod = 1; }
            else if ((initialValue >= 1000) && (initialValue <= 1000000))
            { displayPeriod = 10; }
            else if ((initialValue >= 1000000) && (initialValue <= 100000000))
            { displayPeriod = 1000; }
            else if ((initialValue > 100000000) && (initialValue <= 1000000000))
            { displayPeriod = 100000; }
            else if (initialValue > 1000000000)
            { displayPeriod = 1000000; }

            if (counter1 == displayPeriod)
            {
                if (mode == true)
                {
                    finalValue = new FileInfo(pathFinalValue).Length;
                }
                else if (mode == false)
                {
                    finalValue = counter;
                }

                processPercent = (((finalValue / correctionValue) * 100) / initialValue);
                if (processPercent > 98)
                {
                    processPercent = 99;
                }
                Console.Write("\r");
                Console.Write("выполнено: {0} % ", Math.Truncate(processPercent));
                counter1 = 0;
            }
            else { }
        }
    }
}
