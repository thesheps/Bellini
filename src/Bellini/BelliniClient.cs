using System.Collections.Generic;
using Bellini.Domain;
using RestSharp;

namespace Bellini
{
    public class BelliniClient
    {
        public BelliniClient(string baseUrl)
        {
            _restClient = new RestClient(baseUrl);
        }

        public IList<Project> GetProjects()
        {
            var projects = _restClient.ExecuteAsGet<ProjectsResponse>(new RestRequest("guestAuth/app/rest/projects"), "GET");
            return projects.Data.Project;
        }

        public IList<BuildType> GetBuildTypes(string projectId)
        {
            var request = new RestRequest("guestAuth/app/rest/buildTypes");
            request.AddQueryParameter("locator", $"project:{projectId}");

            var buildTypes = _restClient.ExecuteAsGet<BuildTypesResponse>(request, "GET");
            return buildTypes.Data.BuildType;
        }

        private readonly RestClient _restClient;
    }
}