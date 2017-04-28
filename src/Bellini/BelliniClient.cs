using System;
using System.Collections.Generic;
using System.Globalization;
using Bellini.Domain;
using RestSharp;

namespace Bellini
{
    public class BelliniClient
    {
        private const string DateTimeFormat = "yyyyMMdd'T'HHmmsszzz";

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

        public IList<Build> GetBuilds(string buildTypeId)
        {
            var buildsRequest = new RestRequest("guestAuth/app/rest/builds");
            buildsRequest.AddQueryParameter("locator", $"buildType:{buildTypeId}");

            var builds = _restClient.ExecuteAsGet<BuildsResponse>(buildsRequest, "GET");

            foreach (var build in builds.Data.Build)
            {
                var detailsRequest = new RestRequest("guestAuth/app/rest/builds/id:" + build.Id);
                var details = _restClient.ExecuteAsGet<DetailsResponse>(detailsRequest, "GET");

                build.QueuedDate = DateTime.ParseExact(details.Data.QueuedDate, DateTimeFormat, CultureInfo.InvariantCulture);
                build.StartDate = DateTime.ParseExact(details.Data.StartDate, DateTimeFormat, CultureInfo.InvariantCulture);
                build.FinishDate = DateTime.ParseExact(details.Data.FinishDate, DateTimeFormat, CultureInfo.InvariantCulture);
            }

            return builds.Data.Build;
        }

        private readonly RestClient _restClient;
    }
}