using SortByDate.Lib.Interfaces;
using System;
using System.IO;

namespace SortByDate.Lib
{
    public class ModificationDateExtractor : IDateExtractor
    {
        public DateTime GetDate(string path)
        {
            return File.GetLastWriteTime(path);
        }
    }
}
