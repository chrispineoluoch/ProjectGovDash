using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectGovDash.Data;
using ProjectGovDash.Models.BoundariesData;
using ProjectGovDash.Models.DSHSurvey;
using ProjectGovDash.Models.UserAccounts;
using X.PagedList;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjectGovDash.Controllers
{
    public class DSHSurveysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DSHSurveysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DSHSurveys Analytics
        public IActionResult Dashboard()
        {
            return View();
        }

        // GET: DSHSurveys
        public IActionResult Index(int? page, string Searchterm)
        {
            var dshsurveyDbset = _context.DSHSurveyDbset;
            if (dshsurveyDbset == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DSHSurveyDbset' is null.");
            }

            var query = dshsurveyDbset.AsQueryable();

            if (Searchterm != null)
            {
                query = query.Where(s => s.Region.Contains(Searchterm)
                                         || s.County.Contains(Searchterm));
            }

            return View(query.ToList().ToPagedList(page ?? 1, 10));
        }

        // GET: DSHSurveys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DSHSurveyDbset == null)
            {
                return NotFound();
            }

            var dSHSurvey = await _context.DSHSurveyDbset
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dSHSurvey == null)
            {
                return NotFound();
            }

            return View(dSHSurvey);
        }

        // GET: DSHSurveys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DSHSurveys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,UserName,Editor,DateofCreation,DateofModification,IPAddress,Region,County,DateofSurvey,NoLastMileSites,DistanceLastMile,DistanceMetro,DistanceBackbone")] DSHSurvey dSHSurvey)
        {
            if (ModelState.IsValid)
            {
                string? currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentuser = _context.Users.SingleOrDefault(u => u.Id == currentUserId);

                dSHSurvey.UserId = currentuser?.Id;
                dSHSurvey.UserName = currentuser?.UserName;
                var datetime = DateTime.Now.ToUniversalTime();
                dSHSurvey.DateofCreation = datetime;
                dSHSurvey.DateofModification = datetime;

                string clientIp;
                if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                    clientIp = HttpContext.Request.Headers["X-Forwarded-For"].ToString();
                else
                    clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "IP not available";

                dSHSurvey.IPAddress = clientIp;

                _context.Add(dSHSurvey);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dSHSurvey);
        }

        // GET: DSHSurveys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DSHSurveyDbset == null)
            {
                return NotFound();
            }

            var dSHSurvey = await _context.DSHSurveyDbset.FindAsync(id);
            if (dSHSurvey == null)
            {
                return NotFound();
            }
            return View(dSHSurvey);
        }

        // POST: DSHSurveys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,UserName,Editor,DateofCreation,DateofModification,IPAddress,Region,County,DateofSurvey,NoLastMileSites,DistanceLastMile,DistanceMetro,DistanceBackbone")] DSHSurvey dSHSurvey)
        {
            if (id != dSHSurvey.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dSHSurvey);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DSHSurveyExists(dSHSurvey.Id))
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
            return View(dSHSurvey);
        }

        // GET: DSHSurveys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DSHSurveyDbset == null)
            {
                return NotFound();
            }

            var dSHSurvey = await _context.DSHSurveyDbset
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dSHSurvey == null)
            {
                return NotFound();
            }

            return View(dSHSurvey);
        }

        // POST: DSHSurveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DSHSurveyDbset == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DSHSurveyDbset'  is null.");
            }
            var dSHSurvey = await _context.DSHSurveyDbset.FindAsync(id);
            if (dSHSurvey != null)
            {
                _context.DSHSurveyDbset.Remove(dSHSurvey);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public JsonResult GetChartData(string county)
        {
            if (_context.DSHSurveyDbset == null || _context.DSHSurveyFiberTargetDbset == null)
            {
                return Json(null);
            }

            if (!string.IsNullOrEmpty(county))
            {
                var actualdata = _context.DSHSurveyDbset.AsQueryable();
                var targetdata = _context.DSHSurveyFiberTargetDbset.AsQueryable();

                var countyactualdata = actualdata.Where(s => s.County != null && s.County.Contains(county));
                var countytargetmetrodata = targetdata.Where(s => s.County != null && s.Category != null && s.County.Contains(county) && s.Category.Equals("Metro"));
                var countytargetbackbonedata = targetdata.Where(s => s.County != null && s.Category != null &&s.County.Contains(county) && s.Category.Equals("Backbone"));

                int sumactuallastmilesites = countyactualdata.Sum(s => (int)Math.Truncate(Convert.ToDecimal(s.NoLastMileSites)));
                int sumactualdistancemetro = countyactualdata.Sum(s => (int)Math.Truncate(Convert.ToDecimal(s.DistanceMetro)));
                int sumactualdistancebackbone = countyactualdata.Sum(s => (int)Math.Truncate(Convert.ToDecimal(s.DistanceBackbone)));
                int sumtargetdistancemetro = countytargetmetrodata.Sum(s => (int)Math.Truncate(Convert.ToDecimal(s.Distance)));
                int sumtargetdistancebackbone = countytargetbackbonedata.Sum(s => (int)Math.Truncate(Convert.ToDecimal(s.Distance)));

                var data = new
                {
                    sumactuallastmilesites,
                    sumactualdistancemetro,
                    sumactualdistancebackbone,
                    sumtargetdistancemetro,
                    sumtargetdistancebackbone
                };

                return Json(data);
            }
            else
            {
                var actualdata = _context.DSHSurveyDbset.AsQueryable();

                int sumnolastmilesites = actualdata.Sum(s => (int)Math.Truncate(Convert.ToDecimal(s.NoLastMileSites)));
                int sumdistancemetro = actualdata.Sum(s => (int)Math.Truncate(Convert.ToDecimal(s.DistanceMetro)));
                int sumdistancebackbone = actualdata.Sum(s => (int)Math.Truncate(Convert.ToDecimal(s.DistanceBackbone)));;

                var data = new
                {
                    sumnolastmilesites,
                    sumdistancemetro,
                    sumdistancebackbone
                };

                return Json(data);
            }
        }

        private bool DSHSurveyExists(int id)
        {
            return (_context.DSHSurveyDbset?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
