namespace ProjectGovDash.ModelViews.EventsManager
{
    public class EventsManager
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateofCreation { get; set; }
        public string? IPAddress { get; set; }

        public string? EventType { get; set; }
        public string? EventName { get; set; }
        public string? EventID { get; set; }
        public string? EventModelName { get; set; }
        public string? OriginalPage { get; set; }
        public string? DestinationPage { get; set; }
    }
}
