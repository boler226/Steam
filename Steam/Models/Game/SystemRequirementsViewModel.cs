using System.ComponentModel.DataAnnotations;

namespace Steam.Models.Game
{
    public class SystemRequirementsViewModel
    {
        public int? Id { get; set; }
        public string OperatingSystem { get; set; }
        public string Processor { get; set; }
        public int RAM { get; set; }
        public string VideoCard { get; set; }
        public int DiskSpace { get; set; }
    }
}
