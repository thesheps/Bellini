using Bellini.Domain;
using NUnit.Framework;
using SelfishHttp;

namespace Bellini.Tests
{
    public class GivenBuildTypes
    {
        private const int Port = 12345;

        [Test]
        public void WhenIGetAllBuildTypesForProject_ThenAllBuildTypesAreRetrieved()
        {
            using (var server = new Server(Port))
            {
                server.OnGet("/guestAuth/app/rest/buildTypes").Respond((req, res) =>
                {
                    if (req.Params["locator"] != "project:MyTest")
                        return;

                    res.Headers["Content-Type"] = "application/json";
                    res.Body = MockResponses.BuildTypes;
                });

                var client = new BelliniClient("http://localhost:12345");
                var buildTypes = client.GetBuildTypes("MyTest");

                Assert.That(buildTypes.Count, Is.EqualTo(2));
                AssertBuildType(buildTypes[0], "BuildTypeOne", "Buildtype One", "MyTest", "My Test", "/guestAuth/app/rest/buildTypes/id:MyTest_BuildTypeOne", @"http://teamcity/viewType.html?buildTypeId=MyTest_BuildTypeOne");
                AssertBuildType(buildTypes[1], "BuildTypeTwo", "Buildtype Two", "MyTest", "My Test", "/guestAuth/app/rest/buildTypes/id:MyTest_BuildTypeTwo", @"http://teamcity/viewType.html?buildTypeId=MyTest_BuildTypeTwo");
            }
        }

        private static void AssertBuildType(BuildType buildType, string id, string name, string projectId, string projectName, string href, string webUrl)
        {
            Assert.That(buildType.Id, Is.EqualTo(id));
            Assert.That(buildType.Name, Is.EqualTo(name));
            Assert.That(buildType.ProjectId, Is.EqualTo(projectId));
            Assert.That(buildType.ProjectName, Is.EqualTo(projectName));
            Assert.That(buildType.Href, Is.EqualTo(href));
            Assert.That(buildType.WebUrl, Is.EqualTo(webUrl));
        }
    }
}