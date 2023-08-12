using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectGovDash.Data;
using ProjectGovDash.Models.BoundariesData;

namespace ProjectGovDash.Controllers
{
    public class BoundariesDatasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoundariesDatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult GetRegionBoundariesList()
        {
            List<BoundariesData>? RegionList = _context.BoundariesDataDbset?.ToList();
            var groupedregions = RegionList?.GroupBy(item => item.Region);
            var uniquegroupedregions = groupedregions?.Select(grp => grp.OrderBy(item => item.Region).First());
            ViewBag.RegionOptions = new SelectList(uniquegroupedregions, "Region", "Region");
            return PartialView("RegionOptionPartial");
        }

        public ActionResult GetCountyBoundariesList(string Region)
        {
            List<BoundariesData>? CountyList = _context.BoundariesDataDbset?.Where(x => x.Region == Region).ToList();
            var groupedcounties = CountyList?.GroupBy(item => item.County);
            var uniquegroupedcounties = groupedcounties?.Select(grp => grp.OrderBy(item => item.County).First());
            ViewBag.CountyOptions = new SelectList(uniquegroupedcounties, "County", "County");
            return PartialView("CountyOptionPartial");
        }

        public ActionResult GetSubCountyBoundariesList(string County)
        {
            List<BoundariesData>? SubCountyList = _context.BoundariesDataDbset?.Where(x => x.County == County).ToList();
            var groupedsubcounties = SubCountyList?.GroupBy(item => item.SubCounty);
            var uniquegroupedsubcounties = groupedsubcounties?.Select(grp => grp.OrderBy(item => item.SubCounty).First());
            ViewBag.SubCountyOptions = new SelectList(uniquegroupedsubcounties, "SubCounty", "SubCounty");
            return PartialView("SubCountyOptionPartial");
        }

        public ActionResult GetConstituencyBoundariesList(string County)
        {
            List<BoundariesData>? ConstituencyList = _context.BoundariesDataDbset?.Where(x => x.County == County).ToList();
            var groupedconstituencies = ConstituencyList?.GroupBy(item => item.Constituency);
            var uniquegroupedconstituencies = groupedconstituencies?.Select(grp => grp.OrderBy(item => item.Constituency).First());
            ViewBag.ConstituencyOptions = new SelectList(uniquegroupedconstituencies, "Constituency", "Constituency");
            return PartialView("ConstituencyOptionPartial");
        }

        public ActionResult GetWardBoundariesList(string Constituency)
        {
            List<BoundariesData>? WardList = _context.BoundariesDataDbset?.Where(x => x.Constituency == Constituency).ToList();
            var groupedwards = WardList?.GroupBy(item => item.Ward);
            var uniquegroupedwards = groupedwards?.Select(grp => grp.OrderBy(item => item.Ward).First());
            ViewBag.WardOptions = new SelectList(uniquegroupedwards, "Ward", "Ward");
            return PartialView("WardOptionPartial");
        }

        private bool BoundariesDataExists(int id)
        {
          return (_context.BoundariesDataDbset?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
