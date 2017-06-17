using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BinaryFilesConvertor
{
    //класс 'processMapping' отображает текуший процента выполнения конвертации (загрузки) в процентах.
    public enum ProcessMode
    {
        Converting,
        Generation
    }
    public abstract class ProcessMappingBase
    {
        private readonly long _totalCount;
        private double _processPercent = 0; //текуший процента выполнения
        private int _displayPeriod = 0;     //периодичность отображения
        //todo: почему 2 не говорят ни названия не комментарии
        private int _counter1 = 0;          //счетчик проходов
        protected long Counter = 0;          //счетчик проходов
        protected long CurrentValue = 0;       //текущий размер конечного файла 

        protected abstract double CorrectionValue { get; }

        protected ProcessMappingBase(long totalCount)
        {
            _totalCount = totalCount;
            InitDisplayPeriod(totalCount);
        }

        private void InitDisplayPeriod(long totalCount)
        {
            if (totalCount < 1000)
            {
                _displayPeriod = 1;
            }
            else if ((totalCount >= 1000) && (totalCount <= 1000000))
            {
                _displayPeriod = 10;
            }
            else if ((totalCount >= 1000000) && (totalCount <= 100000000))
            {
                _displayPeriod = 1000;
            }
            else if ((totalCount > 100000000) && (totalCount <= 1000000000))
            {
                _displayPeriod = 100000;
            }
            else if (totalCount > 1000000000)
            {
                _displayPeriod = 1000000;
            }
        }

        public void ProcessMappingInPercent()
        {
            Counter = Counter + 1;
            _counter1 = _counter1 + 1; //todo: counter += 1; counter++; ++counter
            

            if (_counter1 == _displayPeriod )
            {
                RecalculateFinalValue();

                _processPercent = (((CurrentValue / CorrectionValue) * 100) / _totalCount);
                
                if (_processPercent > 98)
                {
                    _processPercent = 99;
                }
                Console.Write("\r");
                Console.Write("выполнено: {0} % ", Math.Truncate(_processPercent));
                _counter1 = 0;
            }
        }

        
        protected abstract void RecalculateFinalValue();
        //{
        //    switch (_mode)
        //    {
        //        case ProcessMode.Converting:
        //            _currentValue = new FileInfo(pathFinalValue).Length;
        //            break;
        //        case ProcessMode.Generation:
        //            _currentValue = _counter;
        //            break;
        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //}
    }

    public class ProcessGenerationMapping : ProcessMappingBase
    {
        public ProcessGenerationMapping(long totalCount) : base(totalCount)
        {
        }

        protected override void RecalculateFinalValue()
        {
            CurrentValue = Counter;
        }

        protected override double CorrectionValue
        {
            get { return 1.0; }
        }
    }

    public class ProcessConvertionMapping : ProcessMappingBase
    {
        private readonly string _pathToFinalFile;

        public ProcessConvertionMapping(string pathToSrcFile, string pathToFinalFile)
            : base(GetCount(pathToSrcFile))
        {
            _pathToFinalFile = pathToFinalFile;
        }

        private static long GetCount(string pathToFile)
        {
            var fi = new FileInfo(pathToFile);
            return fi.Length;
        }

        protected override void RecalculateFinalValue()
        {
            CurrentValue = GetCount(_pathToFinalFile);
        }

        protected override double CorrectionValue
        {
            get { return 1.1; }
        }
    }
}
