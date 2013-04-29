using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderedJobsKata
{
    public class JobOrderer
    {
        private readonly HashSet<string> _jobsSeen = new HashSet<string>();

        public string GetJobOrdering(string jobs)
        {
            return OrderJobs(JobParser.ParseJobs(jobs));
        }

        private string OrderJobs(IList<Job> jobs)
        {
            return jobs.Aggregate("", (current, job) => current + GetJobDependencies(job, current, jobs));
        }

        private string GetJobDependencies(Job current, string result, IList<Job> jobs)
        {
            if (result.Contains(current.Name))
                return "";
            if (string.IsNullOrEmpty(current.DependencyName))
                return current.Name;

            ThrowIfCircularDependencyFound(current);

            return GetJobDependencies(jobs.Single(x => x.Name == current.DependencyName), result, jobs) + current.Name;
        }

        private void ThrowIfCircularDependencyFound(Job current)
        {
            if (_jobsSeen.Contains(current.Name))
                throw new ArgumentException("Circular dependency detected!");
            _jobsSeen.Add(current.Name);
        }
    }
}
