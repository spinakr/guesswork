using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using Web;

namespace IntegrationTests
{
    [TestFixture]
    public abstract class Scenario
    {
        protected HttpClient TestClient;

        [SetUp]
        public void Setup()
        {
            var factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(b =>
            {
                b.UseEnvironment("IntegrationTest");
                b.UseUrls("http://localhost:4000/");
            });
            TestClient = factory.CreateClient();
            TestClient.DefaultRequestHeaders.Accept.Clear();
            TestClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Given();
            When();
        }

        public abstract void Given();
        public abstract void When();
    }
}