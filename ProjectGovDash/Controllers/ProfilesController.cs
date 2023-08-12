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
using ProjectGovDash.Models.UserAccounts;
using ProjectGovDash.ModelViews;

namespace ProjectGovDash.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Profiles
        public async Task<IActionResult> Index()
        {
            string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (_context.ProfilesDbset != null)
            {
                var filteredprofilelist = await _context.ProfilesDbset.Where(x => x.UserId == currentUserId).ToListAsync();

                CompleteProfileViewModel groupmodel = new CompleteProfileViewModel();
                groupmodel.profilelist = filteredprofilelist;
                return View(groupmodel);
            }
            else
            {
                return RedirectToAction("Create");
            }            
        }

        // GET: Profiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProfilesDbset == null)
            {
                return NotFound();
            }

            var profile = await _context.ProfilesDbset
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // GET: Profiles/Create
        public IActionResult Create()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                // QUERY: Check whether the user's profile exists
                string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentuser = _context.Users.SingleOrDefault(u => u.Id == currentUserId);

                if (currentuser != null && _context.ProfilesDbset != null)
                {
                    string captureduserid_check = currentuser.Id;
                    var currentprofile_check = _context.ProfilesDbset.SingleOrDefault(s => s.UserId == captureduserid_check);
                    if (currentprofile_check == null)
                    {
                        return View();
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToPage("/Account/Login", new { area = "Identity" });
                }
            }
            else
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,UserName,Editor,FirstName,LastName,DateofCreation,DateofModification,IPAddress,DateofBirth,Gender,IDNumber,PhoneNumber,AllocatedRole,EventId")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentuser = _context.Users.SingleOrDefault(u => u.Id == currentUserId);

                profile.UserId = currentuser?.Id;
                profile.UserName = currentuser?.UserName;
                var datetime = DateTime.Now.ToUniversalTime();
                profile.DateofCreation = datetime;
                profile.DateofModification = datetime;

                string clientIp;
                if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                    clientIp = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
                else
                    clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP not available";

                profile.IPAddress = clientIp;

                _context.Add(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(profile);
        }

        // GET: Profiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProfilesDbset == null)
            {
                return NotFound();
            }

            var profile = await _context.ProfilesDbset.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,UserName,Editor,FirstName,LastName,DateofCreation,DateofModification,IPAddress,DateofBirth,Gender,IDNumber,PhoneNumber,AllocatedRole,EventId")] Profile profile)
        {
            if (id != profile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid && _context.ProfilesDbset != null)
            {
                try
                {
                    string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var currentuser = _context.Users.SingleOrDefault(u => u.Id == currentUserId);

                    var original_data = _context.ProfilesDbset.AsNoTracking().Where(x => x.Id == profile.Id).FirstOrDefault();

                    profile.UserId = original_data?.UserId;
                    profile.UserName = original_data?.UserName;
                    profile.FirstName = original_data?.FirstName;
                    profile.LastName = original_data?.LastName;
                    profile.DateofCreation = original_data?.DateofCreation;

                    profile.Editor = currentuser?.UserName;
                    profile.DateofModification = DateTime.Now.ToUniversalTime();

                    string clientIp;
                    if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                        clientIp = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
                    else
                        clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP not available";

                    profile.IPAddress = clientIp;

                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(profile);
        }

        // GET: Profiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProfilesDbset == null)
            {
                return NotFound();
            }

            var profile = await _context.ProfilesDbset
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProfilesDbset == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProfilesDbset'  is null.");
            }
            var profile = await _context.ProfilesDbset.FindAsync(id);
            if (profile != null)
            {
                _context.ProfilesDbset.Remove(profile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileExists(int id)
        {
          return _context.ProfilesDbset?.Any(e => e.Id == id) ?? false;
        }
    }
}
