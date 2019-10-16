using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace SortByDate
{
    public class Options
    {
        [Option('q', "quiet", Required = false, Default = false, HelpText = "Mute output.")]
        public bool Quiet { get; set; }

        [Option('f', "force", Required = false, Default = false, HelpText = "Overwrite existing files.")]
        public bool Force { get; set; }

        [Option('c', "copy", Required = false, Default = false, SetName = "copy", HelpText = "Copy files.")]
        public bool Copy { get; set; }

        [Option('m', "move", Required = false, Default = false, SetName = "move", HelpText = "Move files.")]
        public bool Move { get; set; }

        [Value(0, Min = 2)]
        public IEnumerable<string> Paths { get; set; }
    }
}
