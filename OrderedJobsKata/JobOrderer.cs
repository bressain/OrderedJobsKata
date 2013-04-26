using System;
using System.Linq;

namespace OrderedJobsKata
{
    public class JobOrderer
    {
        public string GetJobOrdering(string jobs)
        {
            if (string.IsNullOrEmpty(jobs))
                return "";

            var lines = jobs.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join("", lines.Select(x => x[0]));
        }
    }
}
