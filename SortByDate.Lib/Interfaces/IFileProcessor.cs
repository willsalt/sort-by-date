using System;
using System.Collections.Generic;
using System.Text;

namespace SortByDate.Lib.Interfaces
{
    public interface IFileProcessor
    {
        void Process(Verb verb, string inputPath, string baseOutputPath, OverwriteBehaviour overwriteBehaviour, Action<string> feedback);
    }
}
