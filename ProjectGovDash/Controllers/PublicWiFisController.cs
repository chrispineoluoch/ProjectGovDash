using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using ProjectGovDash.Data;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ProjectGovDash.Models.PublicWiFi;

namespace ProjectGovDash.Controllers
{
    public class PublicWiFisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PublicWiFisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PublicWiFis
        public IActionResult Index(int? page, string Searchterm, string Searchcoordinates)
        {
            var publicWiFiDbSet = _context.PublicWiFiDbset;
            if (publicWiFiDbSet == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PublicWiFiDbset' is null.");
            }

            var query = publicWiFiDbSet.AsQueryable();

            if (Searchterm != null)
            {
                query = query.Where(s => s.Region.Contains(Searchterm)
                                         || s.County.Contains(Searchterm)
                                         || s.Constituency.Contains(Searchterm)
                                         || s.SiteName.Contains(Searchterm));
            }

            return View(query.ToList().ToPagedList(page ?? 1, 10));
        }

        // GET: PublicWiFis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PublicWiFiDbset == null)
            {
                return NotFound();
            }

            var publicWiFi = await _context.PublicWiFiDbset
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicWiFi == null)
            {
                return NotFound();
            }

            return View(publicWiFi);
        }

        // GET: PublicWiFis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PublicWiFis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,UserName,Editor,DateofCreation,DateofModification,IPAddress,Region,County,SubCounty,Constituency,Ward,SiteName,Coordinates,AccessPoints,DateofInstallation,SiteStatus")] PublicWiFi publicWiFi)
        {
            if (ModelState.IsValid)
            {
                string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentuser = _context.Users.SingleOrDefault(u => u.Id == currentUserId);

                publicWiFi.UserId = currentuser?.Id;
                publicWiFi.UserName = currentuser?.UserName;
                var datetime = DateTime.Now.ToUniversalTime();
                publicWiFi.DateofCreation = datetime;
                publicWiFi.DateofModification = datetime;

                string clientIp;
                if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                    clientIp = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
                else
                    clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP not available";

                publicWiFi.IPAddress = clientIp;

                _context.Add(publicWiFi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            return View(publicWiFi);
        }

        // GET: PublicWiFis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PublicWiFiDbset == null)
            {
                return NotFound();
            }

            var publicWiFi = await _context.PublicWiFiDbset.FindAsync(id);
            if (publicWiFi == null)
            {
                return NotFound();
            }
            return View(publicWiFi);
        }

        // POST: PublicWiFis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,UserName,Editor,DateofCreation,DateofModification,IPAddress,Region,County,SubCounty,Constituency,Ward,SiteName,Coordinates,AccessPoints,DateofInstallation,SiteStatus")] PublicWiFi publicWiFi)
        {
            if (id != publicWiFi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var currentuser = _context.Users.SingleOrDefault(u => u.Id == currentUserId);

                    var original_data = _context.ProfilesDbset?.AsNoTracking().Where(x => x.Id == publicWiFi.Id).FirstOrDefault();

                    publicWiFi.UserId = original_data?.UserId;
                    publicWiFi.UserName = original_data?.UserName;
                    publicWiFi.DateofCreation = original_data?.DateofCreation;

                    publicWiFi.Editor = currentuser?.UserName;
                    publicWiFi.DateofModification = DateTime.Now.ToUniversalTime();

                    string clientIp;
                    if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                        clientIp = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
                    else
                        clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP not available";

                    publicWiFi.IPAddress = clientIp;

                    _context.Update(publicWiFi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicWiFiExists(publicWiFi.Id))
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
            return View(publicWiFi);
        }

        // GET: PublicWiFis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PublicWiFiDbset == null)
            {
                return NotFound();
            }

            var publicWiFi = await _context.PublicWiFiDbset
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicWiFi == null)
            {
                return NotFound();
            }

            return View(publicWiFi);
        }

        // POST: PublicWiFis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PublicWiFiDbset == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PublicWiFiDbset'  is null.");
            }
            var publicWiFi = await _context.PublicWiFiDbset.FindAsync(id);
            if (publicWiFi != null)
            {
                _context.PublicWiFiDbset.Remove(publicWiFi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicWiFiExists(int id)
        {
          return (_context.PublicWiFiDbset?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
