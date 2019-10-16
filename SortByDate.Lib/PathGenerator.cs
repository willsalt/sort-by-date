using System;
using System.IO;

namespace SortByDate.Lib
{
    public static class PathGenerator
    {
        public static string GetPath(DateTime date)
        {
            return Path.Combine(date.ToString("yyyy"), date.ToString("yyyy-MM"), date.ToString("yyyy-MM-dd"));
        }
    }
}
