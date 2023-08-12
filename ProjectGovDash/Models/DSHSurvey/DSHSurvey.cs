using System.ComponentModel.DataAnnotations;

namespace ProjectGovDash.Models.DSHSurvey
{
    public class DSHSurvey
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

        [Display(Name = "Date of Survey")]
        public DateTime? DateofSurvey { get; set; }

        [Display(Name = "Number of Last Mile Sites")]
        public string? NoLastMileSites { get; set; }

        [Display(Name = "Distance of Last Mile")]
        public string? DistanceLastMile { get; set; }

        [Display(Name = "Distance of Metro")]
        public string? DistanceMetro { get; set; }

        [Display(Name = "Distance of Backbone")]
        public string? DistanceBackbone { get; set; }
    }
}
