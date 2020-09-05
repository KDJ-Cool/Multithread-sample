using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace MultiThread
{
    internal class Example
    {
        public IList<int> FileData { get; set; }

        public Example(string fileName, bool quick=false)
        {
            FileData = GetFileData(fileName);

            Thread thread;
            if (quick)
            {
                var watch = Stopwatch.StartNew();
                thread = new Thread(() => FileData.ToList().Sort());

                watch.Stop();
                var elapsedTime = watch.ElapsedMilliseconds;
                Console.WriteLine($"{FileData.Count} >> Time Taken: {(double)elapsedTime / 1000} [s]");
            }
            else
            {
                thread = new Thread(BubbleSort);
            }

            thread?.Start();
        }

        private static List<int> GetFileData(string fileName)
        {
            var currentPath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);
            var filePath = currentPath + "\\Resources\\" + fileName + ".txt";
            var streamReader = new StreamReader(filePath);

            var fileData = new List<int>();
            while (!streamReader.EndOfStream)
            {
                Int32.TryParse(streamReader.ReadLine(), out int lineValue);
                fileData.Add(lineValue);
            }

            return fileData;
        }

        private void BubbleSort()
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
        }
    }
}