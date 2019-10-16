using System;

namespace SortByDate.Lib.Interfaces
{
    public interface IDateExtractor
    {
        DateTime GetDate(string path);
    }
}
