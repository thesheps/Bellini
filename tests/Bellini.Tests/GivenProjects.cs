using Bellini.Domain;
using NUnit.Framework;
using SelfishHttp;

namespace Bellini.Tests
{
    public class GivenProjects
    {
        private const int Port = 12345;

        [Test]
        public void WhenIGetAllProjects_ThenAllProjectsAreRetrieved()
        {
            using (var server = new Server(Port))
            {
                server.OnGet("/guestAuth/app/rest/projects").Respond((req, res) =>
                {
                    res.Headers["Content-Type"] = "application/json";
                    res.Body = MockResponses.Projects;
                });

                var client = new BelliniClient("http://localhost:12345");
                var projects = client.GetProjects();

                Assert.That(projects.Count, Is.EqualTo(2));
                AssertProject(projects[0], "_Root", "<Root project>", null, "Contains all other projects", "/guestAuth/app/rest/projects/id:_Root", @"http://teamcity/project.html?projectId=_Root");
                AssertProject(projects[1], "TestProject", "Test Project", "_Root", "Test Project Description", "/guestAuth/app/rest/projects/id:TestProject", @"http://teamcity/project.html?projectId=TestProject");
            }
        }

        private static void AssertProject(Project project, string id, string name, string parentProjectId, string description, string url, string webUrl)
        {
            Assert.That(project.Id, Is.EqualTo(id));
            Assert.That(project.ParentProjectId, Is.EqualTo(parentProjectId));
            Assert.That(project.Name, Is.EqualTo(name));
            Assert.That(project.Description, Is.EqualTo(description));
            Assert.That(project.Href, Is.EqualTo(url));
            Assert.That(project.WebUrl, Is.EqualTo(webUrl));
        }
    }
}