using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace git2ai
{
    public class GitValue
    {
        public string Placeholder { get; set; }

        public string Value { get; set; }

        public GitValue(string placeHolder, string value)
        {
            this.Placeholder = placeHolder;
            this.Value = value;
        }
    }

    public class GitDataProvider
    {
        public IEnumerable<GitValue> GetValues(string gitDir)
        {
            var values = new List<GitValue>();

            using (var repo = new Repository(gitDir))
            {
                var head = repo.Commits.FirstOrDefault();

                var describe = repo.Describe(head, new DescribeOptions()
                {
                    Strategy = DescribeStrategy.Tags,
                    UseCommitIdAsFallback = true
                });

                var dirty = repo.RetrieveStatus().IsDirty ? "-dirty" : string.Empty;

                values.Add(new GitValue("{describe-tags}", describe));
                values.Add(new GitValue("{dirty}", dirty));
            }

            return values;
        }
    }
}
