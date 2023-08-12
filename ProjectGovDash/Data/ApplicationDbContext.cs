using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectGovDash.Models.BoundariesData;
using ProjectGovDash.Models.DSHSurvey;
using ProjectGovDash.Models.PublicWiFi;
using ProjectGovDash.Models.UserAccounts;
using ProjectGovDash.ModelViews;
using ProjectGovDash.ModelViews.EventsManager;

namespace ProjectGovDash.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Profile>? ProfilesDbset { get; set; }
        public DbSet<EventsManager>? EventsDbset { get; set; }
        public DbSet<PublicWiFi>? PublicWiFiDbset { get; set; }
        public DbSet<PublicWiFiGallery>? PublicWiFiGalleryDbset { get; set; }
        public DbSet<DSHSurvey>? DSHSurveyDbset { get; set; }
        public DbSet<BoundariesData>? BoundariesDataDbset { get; set; }
        public DbSet<DSHSurveyFiberTarget>? DSHSurveyFiberTargetDbset { get; set; }
    }
}