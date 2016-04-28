﻿using System;
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

        public void ReplacePlaceholders(string[] files, IEnumerable<GitValue> gitValues)
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
                if(!replaced)
                {
                    Console.WriteLine("- No placeholder found");
                }

                File.WriteAllText(filePath, content, Encoding.UTF8);
            }
        }
    }
}
