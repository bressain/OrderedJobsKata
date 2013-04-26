using System;
using System.Collections.Generic;
using System.Linq;

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

        private string OrderJobs(IList<Job> jobs)
        {
            return jobs.Aggregate("", (current, j) => current + GetJobDependencies(j, current, jobs));
        }

        private string GetJobDependencies(Job current, string result, IList<Job> jobs)
        {
            if (result.Contains(current.Name))
                return "";
            if (string.IsNullOrEmpty(current.DependencyName))
                return current.Name;
            return GetJobDependencies(jobs.Single(x => x.Name == current.DependencyName), result, jobs) + current.Name;
        }

        private static string[] SplitJobs(string jobs)
        {
            return jobs.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private IList<Job> ParseJobs(string[] jobLines)
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
