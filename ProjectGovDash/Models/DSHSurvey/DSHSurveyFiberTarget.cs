namespace ProjectGovDash.Models.DSHSurvey
{
    public class DSHSurveyFiberTarget
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
        public string? Category { get; set; }
        public string? Nature { get; set; }
        public string? SectionName { get; set; }
        public string? Distance { get; set; }
    }
}
