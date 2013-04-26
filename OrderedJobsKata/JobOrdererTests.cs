using NUnit.Framework;

namespace OrderedJobsKata
{
    [TestFixture]
    public class JobOrdererTests
    {
        private JobOrderer _jobOrderer;

        [SetUp]
        public void Setup()
        {
            _jobOrderer = new JobOrderer();
        }

        [Test]
        public void Empty_input_gives_empty_output()
        {
            Assert.That(_jobOrderer.GetJobOrdering(""), Is.EqualTo(""));
        }

        [Test]
        public void Single_job_gives_one_output()
        {
            Assert.That(_jobOrderer.GetJobOrdering("a =>"), Is.EqualTo("a"));
        }

        [Test]
        public void Multiple_jobs_gives_jobs_in_given_order()
        {
            const string JOBS =
                "a =>\n" +
                "b =>\n" +
                "c =>\n";
            Assert.That(_jobOrderer.GetJobOrdering(JOBS), Is.EqualTo("abc"));
        }
    }
}
