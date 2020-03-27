using System;
using System.Net.Http;
using NUnit.Framework;

namespace IntegrationTests
{
    public class CreateTournament : Scenario
    {
        public override void Given()
        {
        }

        public override void When()
        {
            var result = TestClient.PostAsync("/api/tournaments", ContentHelper.GetStringContent(
                new
                {
                    Name = "IntTestTroueny",
                    Starts = DateTime.Now,
                    Ends = DateTime.Now,
                })).GetAwaiter().GetResult();
        }

        [Test]
        public void Test()
        {
        }
    }
}