using CommandLine;
using SortByDate.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SortByDate
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string str in args)
            {
                Console.Error.WriteLine(str);
            }
            ParserResult<Options> cliParser = Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o => RunTool(o))
                .WithNotParsed(e => ExitWithError(e));
        }

        private static void ExitWithError(IEnumerable<Error> errs)
        {
            Environment.Exit(0);
        }

        private static void RunTool(Options o)
        {
            if (!o.Copy && !o.Move)
            {
                Console.Error.WriteLine("One of either --copy or --move must be specified.");
                Environment.Exit(0);
            }
            if (o.Move)
            {
                Console.Error.WriteLine("--move is not yet implemented.");
                Environment.Exit(0);
            }
            string[] allClPaths = o.Paths.ToArray();
            foreach (string str in allClPaths)
            {
                Console.Error.WriteLine(str);
            }
            IEnumerable<string> clInputPaths = allClPaths.Take(allClPaths.Length - 1);
            string clOutputPath = allClPaths.Last();
            IEnumerable<string> inputFiles = GetInputFilesFromInputPaths(clInputPaths);

            FileProcessor processor = new FileProcessor(new ModificationDateExtractor());
            OverwriteBehaviour overwrite = o.Force ? OverwriteBehaviour.Permit : OverwriteBehaviour.Error;
            Action<string> feedbackMethod = o.Quiet ? (Action<string>)null : PrintFeedback;
            foreach (string fn in inputFiles)
            {
                processor.Process(Verb.Copy, fn, clOutputPath, overwrite, feedbackMethod);
            }
        }

        private static void PrintFeedback(string msg)
        {
            Console.WriteLine(msg);
        }

        private static IEnumerable<string> GetInputFilesFromInputPaths(IEnumerable<string> clInputPaths)
        {
            // It's not worth implementing my own OrderedDictionary<K,V> right now
            List<string> fileList = new List<string>();
            Dictionary<string, string> fileDict = new Dictionary<string, string>();
            foreach (string path in clInputPaths)
            {
                if (File.Exists(path))
                {
                    AddToList(path, fileList, fileDict);
                    continue;
                }
                if (Directory.Exists(path))
                {
                    foreach (string f in Directory.EnumerateFiles(path, "*", SearchOption.AllDirectories))
                    {
                        AddToList(f, fileList, fileDict);
                    }
                }
                Console.Error.WriteLine($"Path {path} does not exist.");
            }

            return fileList;
        }

        private static void AddToList(string path, List<string> fileList, Dictionary<string, string> fileDict)
        {
            if (!fileDict.ContainsKey(path))
            {
                fileList.Add(path);
                fileDict.Add(path, path);
            }
        }
    }
}
