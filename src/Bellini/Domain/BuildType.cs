using System.Collections.Generic;

namespace Bellini.Domain
{
    internal class BuildTypesResponse
    {
        public List<BuildType> BuildType { get; set; }
    }

    public class BuildType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Href { get; set; }
        public string WebUrl { get; set; }
    }
}