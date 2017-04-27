using Bellini.Domain;
using NUnit.Framework;
using SelfishHttp;

namespace Bellini.Tests
{
    public class GivenBuilds
    {
        private const int Port = 12345;

        [Test]
        public void WhenIGetAllBuildsForBuildType_ThenAllBuildsAreRetrieved()
        {
            using (var server = new Server(Port))
            {
                server.OnGet("/guestAuth/app/rest/builds").Respond((req, res) =>
                {
                    if (req.Params["locator"] != "buildType:MyTest_MyBuildType")
                        return;

                    res.Headers["Content-Type"] = "application/json";
                    res.Body = MockResponses.Builds;
                });

                var client = new BelliniClient("http://localhost:12345");
                var builds = client.GetBuilds("MyTest_MyBuildType");

                Assert.That(builds.Count, Is.EqualTo(2));
                AssertBuild(builds[0], 1, "MyTest_BuildType1", "2", "SUCCESS", "finished", "master", true, "/guestAuth/app/rest/builds/id:1", @"http://teamcity/viewLog.html?buildId=1&buildTypeId=MyTest_BuildType1");
                AssertBuild(builds[1], 2, "MyTest_BuildType1", "3", "SUCCESS", "finished", "master", true, "/guestAuth/app/rest/builds/id:2", @"http://teamcity/viewLog.html?buildId=2&buildTypeId=MyTest_BuildType1");
            }
        }

        private static void AssertBuild(Build build, int id, string buildTypeId, string number, string status, string state, string branchName, bool defaultBranch, string href, string webUrl)
        {
            Assert.That(build.Id, Is.EqualTo(id));
            Assert.That(build.BuildTypeId, Is.EqualTo(buildTypeId));
            Assert.That(build.Number, Is.EqualTo(number));
            Assert.That(build.Status, Is.EqualTo(status));
            Assert.That(build.State, Is.EqualTo(state));
            Assert.That(build.BranchName, Is.EqualTo(branchName));
            Assert.That(build.DefaultBranch, Is.EqualTo(defaultBranch));
            Assert.That(build.Href, Is.EqualTo(href));
            Assert.That(build.WebUrl, Is.EqualTo(webUrl));
        }
    }
}