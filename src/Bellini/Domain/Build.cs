using System;
using System.Collections.Generic;

namespace Bellini.Domain
{
    internal class BuildsResponse
    {
        public List<Build> Build { get; set; }
    }

    public class Build
    {
        public int Id { get; set; }
        public string BuildTypeId { get; set; }
        public string Number { get; set; }
        public string Status { get; set; }
        public string State { get; set; }
        public string BranchName { get; set; }
        public bool DefaultBranch { get; set; }
        public string Href { get; set; }
        public string WebUrl { get; set; }
        public DateTime QueuedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
    }
}