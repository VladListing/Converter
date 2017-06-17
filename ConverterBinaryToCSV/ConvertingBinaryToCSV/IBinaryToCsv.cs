using System.Threading.Tasks;

namespace ConvertingBinaryToCsvLibrary
{
    //интерфейс 'IBinaryToCsv'

    public interface IBinaryToCsv
    {
        void FromBinaryFileToCsv(string pathDat, string pathCsv);
    }

    public interface IConverter
    {
        void Convert(string srcPath, string destPath);
        Task ConvertAsync(string srcPath, string destPath);
    }

    public class BinaryToCsvConverter : IConverter
    {
        public void Convert(string srcPath, string destPath)
        {
            throw new System.NotImplementedException();
        }

        public Task ConvertAsync(string srcPath, string destPath)
        {
            throw new System.NotImplementedException();
        }
    }

    public class CsvToBinaryConverter : IConverter
    {
        public void Convert(string srcPath, string destPath)
        {
            throw new System.NotImplementedException();
        }

        public Task ConvertAsync(string srcPath, string destPath)
        {
            throw new System.NotImplementedException();
        }
    }
}
