namespace OrderedJobsKata
{
    public class JobOrderer
    {
        public string GetJobOrdering(string jobs)
        {
            if (string.IsNullOrEmpty(jobs))
                return "";
            return jobs[0].ToString();
        }
    }
}
