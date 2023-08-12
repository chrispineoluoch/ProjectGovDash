using System.ComponentModel.DataAnnotations;

namespace ProjectGovDash.Models.UserAccounts
{
    public class Profile
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Editor { get; set; }

        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        public string? FirstName { get; set; }

        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        public string? LastName { get; set; }
        public DateTime? DateofCreation { get; set; }
        public DateTime? DateofModification { get; set; }
        public string? IPAddress { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yy}", ApplyFormatInEditMode = true)]
        public DateTime? DateofBirth { get; set; }
        public string? Gender { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "Only numbers are allowed.")]
        public string? IDNumber { get; set; }

        [RegularExpression("^[0-9]+$", ErrorMessage = "Only numbers are allowed.")]
        public string? PhoneNumber { get; set; }
        public string? AllocatedRole { get; set; }
        public string? EventId { get; set; }
    }
}
