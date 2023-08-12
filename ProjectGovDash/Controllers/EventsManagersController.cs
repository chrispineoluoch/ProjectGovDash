using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectGovDash.Data;
using ProjectGovDash.Models;
using ProjectGovDash.ModelViews.EventsManager;

namespace ProjectGovDash.Controllers
{
    public class EventsManagersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsManagersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public JsonResult PostEvent(string eventtype, string eventname, string eventmodelname, string eventid, string originalpage, string destinationpage, EventsManager eventsManager, IHttpContextAccessor httpContextAccessor)
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentuser = _context.Users.SingleOrDefault(u => u.Id == currentUserId);

            var currentuserprofile = _context.ProfilesDbset?.AsNoTracking().Where(x => x.Id.ToString() == currentUserId).FirstOrDefault();

            eventsManager.UserId = currentuser?.Id.ToString();
            eventsManager.UserName = currentuser?.UserName;
            eventsManager.FirstName = currentuserprofile?.FirstName;
            eventsManager.LastName = currentuserprofile?.LastName;
            eventsManager.DateofCreation = DateTime.Now.ToUniversalTime();
            eventsManager.IPAddress = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

            eventsManager.EventID = eventid;
            eventsManager.EventType = eventtype;
            eventsManager.EventName = eventname;
            eventsManager.EventModelName = eventmodelname;
            eventsManager.OriginalPage = originalpage;
            eventsManager.DestinationPage = destinationpage;

            _context.EventsDbset?.Add(eventsManager);
            _context.SaveChanges();

            var success = "Success";

            return Json(success);
        }
    }
}
