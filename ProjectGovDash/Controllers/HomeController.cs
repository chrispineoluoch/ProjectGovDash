using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectGovDash.Data;
using ProjectGovDash.Models;
using ProjectGovDash.ModelViews;
using System.Configuration;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProjectGovDash.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            string State = "";

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentuser = _context.Users.SingleOrDefault(u => u.Id == currentUserId);

                if (currentuser != null && _context.ProfilesDbset != null)
                {
                    string captureduserid_check = currentuser.Id;
                    var currentprofile_check = _context.ProfilesDbset.SingleOrDefault(s => s.UserId == captureduserid_check);
                    if (currentprofile_check != null)
                    {
                        State = "toggle-hide";
                    }
                    else
                    {
                        State = "toggle-show";
                    }
                }
                else
                {
                    State = "toggle-show";
                }
            }
            else
            {
                State = "toggle-hide";
            }

            ViewBag.State = State;

            return View();
        }

        public IActionResult Consultations()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentuser = _context.Users.SingleOrDefault(u => u.Id == currentUserId);
                var userid = currentuser?.Id;

                var profiledata = _context.ProfilesDbset?.Where(x => x.UserId == userid).FirstOrDefault();
                var firstname = profiledata?.FirstName;
                ViewBag.FirstName = firstname;

                string State = "";

                if (User.Identity != null && User.Identity.IsAuthenticated)
                {
                    string? thecurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var thecurrentuser = _context.Users.SingleOrDefault(u => u.Id == thecurrentUserId);

                    if (thecurrentuser != null && _context.ProfilesDbset != null)
                    {
                        string captureduserid_check = thecurrentuser.Id;
                        var currentprofile_check = _context.ProfilesDbset.SingleOrDefault(s => s.UserId == captureduserid_check);
                        if (currentprofile_check != null)
                        {
                            State = "toggle-hide";
                        }
                        else
                        {
                            State = "toggle-show";
                        }
                    }
                    else
                    {
                        State = "toggle-show";
                    }
                }
                else
                {
                    State = "toggle-hide";
                }

                ViewBag.State = State;

                return View();
            }
            else
            {
                // This code redirects the user to the login page before allowing them to access the consultations page
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}