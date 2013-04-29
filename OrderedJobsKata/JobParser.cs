using System;
using System.Collections.Generic;

namespace OrderedJobsKata
{
    public class JobParser
    {
        public static IList<Job> ParseJobs(string jobs)
        {
            var jobLines = SplitJobs(jobs);
            return ParseJobLines(jobLines);
        }

        private static string[] SplitJobs(string jobs)
        {
            return jobs.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static IList<Job> ParseJobLines(string[] jobLines)
        {
            var jobs = new List<Job>(jobLines.Length);
            foreach (var line in jobLines)
            {
                var job = ParseJob(line);
                if (job.Name == job.DependencyName)
                    throw new ArgumentException("Job can't depend on itself");
                jobs.Add(job);
            }
            return jobs;
        }

        private static Job ParseJob(string jobLine)
        {
            var split = jobLine.Split(new[] { "=>" }, StringSplitOptions.RemoveEmptyEntries);
            var job = new Job { Name = split[0].Trim() };
            if (split.Length > 1)
                job.DependencyName = split[1].Trim();
            return job;
        }
    }
}
