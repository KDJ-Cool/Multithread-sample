using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThread
{
    internal class AsyncExample
    {
        private readonly IList<int> _exData;

        public AsyncExample(string fileName)
        {
            Task<List<int>> fileDataTask = GetFileDataAsync(fileName);
            Console.WriteLine("Getting file data async.");

            fileDataTask.Wait();
            List<int> fileData = fileDataTask.Result;
            _exData = fileData;

            BubbleSort(fileData);
        }

        private static async Task<List<int>> GetFileDataAsync(string fileName)
        {
            var currentPath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            var filePath = currentPath + "\\Resources\\" + fileName + ".txt";
            var streamReader = new StreamReader(filePath);

            var fileData = new List<int>();
            while (!streamReader.EndOfStream)
            {
                var line = await streamReader.ReadLineAsync();
                Int32.TryParse(line, out int lineValue);
                fileData.Add(lineValue);
            }

            return fileData;
        }

        public static List<int> BubbleSort(List<int> FileData)
        {
            var watch = Stopwatch.StartNew();

            for (int j = 0; j <= FileData.Count - 2; j++)
            {
                for (int i = 0; i <= FileData.Count - 2; i++)
                {
                    if (FileData[i] <= FileData[i + 1]) continue;
                    var temp = FileData[i + 1];
                    FileData[i + 1] = FileData[i];
                    FileData[i] = temp;
                }
            }

            watch.Stop();
            var elapsedTime = watch.ElapsedMilliseconds;
            Console.WriteLine($"{FileData.Count} >> Time Taken: {(double)elapsedTime / 1000} [s]");

            return FileData;
        }

        public int GetFirstElement()
        {
            return _exData[0];
        }

        public async Task<int> GiveFirstElementAsync()
        {
            var firstElement  = await Task.Run(GetFirstElement);
            return _exData[0];
        }
    }
}