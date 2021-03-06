﻿using System;
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
                "c =>";
            Assert.That(_jobOrderer.GetJobOrdering(JOBS), Is.EqualTo("abc"));
        }

        [Test]
        public void Multiple_jobs_with_dependency_use_dependency_first()
        {
            const string JOBS =
                "a =>\n" +
                "b => c\n" +
                "c =>";
            Assert.That(_jobOrderer.GetJobOrdering(JOBS), Is.EqualTo("acb"));
        }

        [Test]
        public void Multiple_jobs_with_dependencies_use_dependencies_first()
        {
            const string JOBS =
                "a =>\n" +
                "b => c\n" +
                "c => f\n" +
                "d => a\n" +
                "e => b\n" +
                "f =>";
            Assert.That(_jobOrderer.GetJobOrdering(JOBS), Is.EqualTo("afcbde"));
        }

        [Test]
        public void Self_referencing_jobs_are_not_allowed()
        {
            const string JOBS =
                "a =>\n" +
                "b =>\n" +
                "c => c";
            Assert.Throws<ArgumentException>(() => _jobOrderer.GetJobOrdering(JOBS));
        }

        [Test]
        public void Circular_dependencies_are_not_allowed()
        {
            const string JOBS =
                "a =>\n" +
                "b => c\n" +
                "c => f\n" +
                "d => a\n" +
                "e =>\n" +
                "f => b";
            Assert.Throws<ArgumentException>(() => _jobOrderer.GetJobOrdering(JOBS));
        }

    }
}
