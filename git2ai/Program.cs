using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git2ai
{
    public class Options
    {
        [Option('g', "gitdir", Required = true,
            HelpText = OptionDescriptions.GitDir)]
        public string GitDir { get; set; }

        [Option('a', "assemblyinfodir", Required = true,
            HelpText = OptionDescriptions.AssemblyInfoDir)]
        public string AssemblyInfoRootDir { get; set; }

        [Option('r', "recursive", Required = false, DefaultValue = "true",
            HelpText = OptionDescriptions.SearchRecursive)]
        public string SearchRecursive { get; set; }

        [Option('p', "searchpattern", Required = false, DefaultValue = "*AssemblyInfo.cs",
            HelpText = OptionDescriptions.SearchPattern)]
        public string SearchPattern { get; set; }

        [Option('o', "output", Required = false, DefaultValue = null,
            HelpText = OptionDescriptions.Output)]
        public string Output { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            if (Parser.Default.ParseArguments(args, options))
            {
                try
                {
                    Process(
                        options.GitDir,
                        options.AssemblyInfoRootDir,
                        options.SearchRecursive.Equals("true", StringComparison.InvariantCultureIgnoreCase),
                        options.SearchPattern,
                        options.Output
                    );
                }
                catch(Exception exc)
                {
                    Console.WriteLine($"Failed to process file. Exception: {exc.ToString()}");
                }
            }
        }

        static void Process(string gitDir, string assemblyInfoDir, bool searchRecursive, string searchPattern, string outputPath)
        {
            if(!Directory.Exists(gitDir))
            {
                Console.WriteLine("GitDir not valid.");
                return;
            }
            if(!Directory.Exists(assemblyInfoDir))
            {
                Console.WriteLine("AssemblyInfoDir not valid.");
                return;
            }

            var replacer = new Replacer();
            var files = replacer.FindFiles(assemblyInfoDir, searchPattern, searchRecursive);

            if (files.Any())
            {
                Console.WriteLine($"Found {files.Count()} file(s):");
                foreach (var file in files)
                {
                    Console.WriteLine($"- {file}");
                }

                var valueProvider = new GitDataProvider();
                var values = valueProvider.GetValues(gitDir);

                replacer.ReplacePlaceholders(files, values, outputPath);
            }
            else
            {
                Console.WriteLine("No files found.");
            }

            Console.WriteLine("Completed");
        }
    }
}
