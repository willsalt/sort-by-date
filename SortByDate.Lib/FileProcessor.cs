using SortByDate.Lib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SortByDate.Lib
{
    public class FileProcessor : IFileProcessor
    {
        private readonly IDateExtractor _dateExtractor;

        public FileProcessor(IDateExtractor dateExtractor)
        {
            _dateExtractor = dateExtractor;
        }

        public void Process(Verb verb, string inputPath, string baseOutputPath, OverwriteBehaviour overwriteBehaviour, Action<string> feedback)
        {
            DateTime fileDateTime = _dateExtractor.GetDate(inputPath);
            string fileName = Path.GetFileName(inputPath);
            string outputFolder = Path.Combine(baseOutputPath, PathGenerator.GetPath(fileDateTime));
            Directory.CreateDirectory(outputFolder);
            string outputPath = Path.Combine(outputFolder, fileName);
            File.Copy(inputPath, outputPath, overwriteBehaviour == OverwriteBehaviour.Permit);
            feedback?.Invoke($"Copied {fileName} to {outputFolder}");
        }
    }
}
