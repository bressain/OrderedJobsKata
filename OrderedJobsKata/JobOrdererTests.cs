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
    }
}
