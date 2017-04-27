using System.Collections.Generic;

namespace Bellini.Domain
{
    public class ProjectResponse
    {
        public List<Project> Project { get; set; }
    }

    public class Project
    {
        public string Id { get; set; }
        public string ParentProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Href { get; set; }
        public string WebUrl { get; set; }
    }
}