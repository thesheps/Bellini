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
            var projects = _restClient.ExecuteAsGet<ProjectResponse>(new RestRequest("guestAuth/app/rest/projects"), "GET");
            return projects.Data.Project;
        }

        private readonly RestClient _restClient;
    }
}