using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProjectGovDash.Models.PublicWiFi
{
    public class PublicWiFiGallery
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Editor { get; set; }
        public DateTime? DateofCreation { get; set; }
        public DateTime? DateofModification { get; set; }
        public string? IPAddress { get; set; }
        public string? SiteId { get; set; }
        public string? SiteImage { get; set; }

        [UIHint("tinymce_jquery_full"), AllowHtml]
        [DataType(DataType.MultilineText)]
        public string? About { get; set; }
    }
}
