using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace git2ai
{
    public class Replacer
    {
        public string[] FindFiles(string assemblyInfoDir, string searchPattern, bool searchRecursive)
        {
            var files = Directory.GetFiles(
                assemblyInfoDir,
                searchPattern,
                searchRecursive ?
                    SearchOption.AllDirectories :
                    SearchOption.TopDirectoryOnly);
            return files;
        }

        public void ReplacePlaceholders(string[] files, IEnumerable<GitValue> gitValues, string outputPath)
        {
            foreach (var filePath in files)
            {
                Console.WriteLine($"Replacing placeholders in {filePath}:");

                var content = File.ReadAllText(filePath);

                bool replaced = false;
                foreach (var gitValue in gitValues)
                {
                    if (content.Contains(gitValue.Placeholder))
                    {
                        replaced = true;
                        content = content.Replace(gitValue.Placeholder, gitValue.Value);

                        Console.WriteLine($"- Replaced \"{gitValue.Placeholder}\" with \"{gitValue.Value}\"");
                    }
                }
                if (!replaced)
                {
                    Console.WriteLine("- No placeholder found");
                }

                if (string.IsNullOrWhiteSpace(outputPath))
                {
                    outputPath = filePath;
                }
                else
                {
                    if (outputPath.StartsWith("\\"))
                    {
                        var basePath = new FileInfo(filePath).DirectoryName;
                        // Path.Combine will return the second argument if it begins with a separation character (\). (see: http://stackoverflow.com/questions/6929262/why-wont-this-path-combine-work)
                        outputPath = Path.Combine(basePath, outputPath.Replace("\\", string.Empty));
                    }

                    // Create directory if necessary.
                    Directory.CreateDirectory(new FileInfo(outputPath).DirectoryName);
                }

                File.WriteAllText(outputPath, content, Encoding.UTF8);
            }
        }
    }
}
