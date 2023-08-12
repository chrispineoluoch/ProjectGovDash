using System.ComponentModel.DataAnnotations;

namespace ProjectGovDash.Models.PublicWiFi
{
    public class PublicWiFi
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Editor { get; set; }
        public DateTime? DateofCreation { get; set; }
        public DateTime? DateofModification { get; set; }
        public string? IPAddress { get; set; }
        public string? Region { get; set; }
        public string? County { get; set; }
        public string? SubCounty { get; set; }
        public string? Constituency { get; set; }
        public string? Ward { get; set; }

        [Display(Name = "Public WiFi Site")]
        public string? SiteName { get; set; }

        [Display(Name = "Site Coordinates")]
        public string? Coordinates { get; set; }

        [Display(Name = "Access Points")]
        public string? AccessPoints { get; set; }

        [Display(Name = "Date of Installation")]
        public string? DateofInstallation { get; set; }

        [Display(Name = "Site Status")]
        public string? SiteStatus { get; set; }
    }
}
