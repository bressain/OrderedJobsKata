using System;
using System.Collections.Generic;

namespace OrderedJobsKata
{
    public class JobOrderer
    {
        public string GetJobOrdering(string jobs)
        {
            if (string.IsNullOrEmpty(jobs))
                return "";

            var lines = SplitJobs(jobs);
            var parsed = ParseJobs(lines);
            return OrderJobs(parsed);
        }

        private string OrderJobs(IEnumerable<Job> jobs)
        {
            string result = "";
            foreach (var j in jobs)
            {
                if (!string.IsNullOrEmpty(j.DependencyName))
                    result += j.DependencyName;
                if (!result.Contains(j.Name))
                    result += j.Name;
            }
            return result;
        }

        private static string[] SplitJobs(string jobs)
        {
            return jobs.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private IEnumerable<Job> ParseJobs(string[] jobLines)
        {
            var jobs = new List<Job>(jobLines.Length);
            foreach (var jl in jobLines)
            {
                var split = jl.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
                var job = new Job { Name = split[0].Trim() };
                if (split.Length > 1)
                    job.DependencyName = split[1].Trim();
                jobs.Add(job);
            }
            return jobs;
        }

        private class Job
        {
            public string Name { get; set; }
            public string DependencyName { get; set; }
        }
    }
}
