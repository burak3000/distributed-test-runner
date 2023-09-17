using System;

namespace DistributedTestRunner.Core.Domain
{
    public class TestRequest
    {
        public string TestName { get; set; }
        public Guid Id { get; set; }
    }
}

