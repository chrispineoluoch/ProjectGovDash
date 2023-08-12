using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectGovDash.Data;
using System.Security.Claims;
using System.Security.Policy;
using ProjectGovDash.Models.PublicWiFi;

namespace ProjectGovDash.Controllers
{
    public class PublicWiFiGalleriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public PublicWiFiGalleriesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: PublicWiFiGalleries
        public async Task<IActionResult> Index(int siteid)
        {
            string capturedsiteid = Convert.ToString(siteid);

            return _context.PublicWiFiGalleryDbset != null ?
                        View(await _context.PublicWiFiGalleryDbset.Where(x => x.SiteId.Equals(capturedsiteid)).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.PublicWiFiGalleryDbset'  is null.");
        }

        // GET: PublicWiFiGalleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PublicWiFiGalleryDbset == null)
            {
                return NotFound();
            }

            var publicWiFiGallery = await _context.PublicWiFiGalleryDbset
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicWiFiGallery == null)
            {
                return NotFound();
            }

            return View(publicWiFiGallery);
        }

        // GET: PublicWiFiGalleries/Create
        public IActionResult Create(int siteid)
        {
            TempData["capturedsiteid"] = siteid;
            return View();
        }

        // POST: PublicWiFiGalleries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,UserName,Editor,DateofCreation,DateofModification,IPAddress,SiteId,SiteImage,About")] PublicWiFiGallery publicWiFiGallery, IFormFile site_Image)
        {
            if (ModelState.IsValid)
            {
                var passeddata = TempData["capturedsiteid"];
                int siteid = Convert.ToInt32(passeddata);
                string? activesiteid = Convert.ToString(passeddata);

                if (site_Image != null && site_Image.Length > 0)
                {
                    string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var currentuser = _context.Users.SingleOrDefault(u => u.Id == currentUserId);

                    var datetime = DateTime.Now.ToUniversalTime().ToString("yyyyMMddHHmmssfff");
                    string? userimagename = datetime + ".jpg";
                    string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "publicwifi");
                    string filePath = Path.Combine(folderPath, userimagename);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await site_Image.CopyToAsync(fileStream);
                    }

                    publicWiFiGallery.SiteId = activesiteid;
                    publicWiFiGallery.SiteImage = userimagename;

                    publicWiFiGallery.UserId = currentuser?.Id;
                    publicWiFiGallery.UserName = currentuser?.UserName;
                    var datetimenow = DateTime.Now.ToUniversalTime();
                    publicWiFiGallery.DateofCreation = datetimenow;
                    publicWiFiGallery.DateofModification = datetimenow;

                    string clientIp;
                    if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                        clientIp = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
                    else
                        clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP not available";

                    publicWiFiGallery.IPAddress = clientIp;
                }

                _context.Add(publicWiFiGallery);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Create));
                return RedirectToAction("Create", new { siteid = siteid });
            }
            return View(publicWiFiGallery);
        }

        // GET: PublicWiFiGalleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PublicWiFiGalleryDbset == null)
            {
                return NotFound();
            }

            var publicWiFiGallery = await _context.PublicWiFiGalleryDbset.FindAsync(id);
            if (publicWiFiGallery == null)
            {
                return NotFound();
            }
            return View(publicWiFiGallery);
        }

        // POST: PublicWiFiGalleries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,UserName,Editor,DateofCreation,DateofModification,IPAddress,SiteId,SiteImage,About")] PublicWiFiGallery publicWiFiGallery)
        {
            if (id != publicWiFiGallery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publicWiFiGallery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicWiFiGalleryExists(publicWiFiGallery.Id))
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
            return View(publicWiFiGallery);
        }

        // GET: PublicWiFiGalleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PublicWiFiGalleryDbset == null)
            {
                return NotFound();
            }

            var publicWiFiGallery = await _context.PublicWiFiGalleryDbset
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publicWiFiGallery == null)
            {
                return NotFound();
            }

            return View(publicWiFiGallery);
        }

        // POST: PublicWiFiGalleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PublicWiFiGalleryDbset == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PublicWiFiGalleryDbset'  is null.");
            }
            var publicWiFiGallery = await _context.PublicWiFiGalleryDbset.FindAsync(id);
            if (publicWiFiGallery != null)
            {
                _context.PublicWiFiGalleryDbset.Remove(publicWiFiGallery);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicWiFiGalleryExists(int id)
        {
            return (_context.PublicWiFiGalleryDbset?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
