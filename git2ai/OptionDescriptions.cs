using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git2ai
{
    public class OptionDescriptions
    {
        public const string GitDir = "Path to.git directory";

        public const string AssemblyInfoDir = "Root to search for AssemblyInfo.cs files";

        public const string SearchRecursive = "If true: Includes sub directories from assemblyinfodir";

        public const string SearchPattern = "Search pattern to use for finding AssemblyInfo files";

        public const string Output = "Defines the output file. If this value is set, the placeholders in the original file will not be replaced. If the output path starts with a slash (\\), the generated file will be placed relative to the file which was used as template.";
    }
}
