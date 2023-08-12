using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectGovDash.Data;
using ProjectGovDash.Models.DSHSurvey;

namespace ProjectGovDash.Controllers
{
    public class DSHSurveyFiberTargetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DSHSurveyFiberTargetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DSHSurveyFiberTargets
        public async Task<IActionResult> Index()
        {
              return _context.DSHSurveyFiberTargetDbset != null ? 
                          View(await _context.DSHSurveyFiberTargetDbset.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DSHSurveyFiberTargetDbset'  is null.");
        }

        // GET: DSHSurveyFiberTargets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DSHSurveyFiberTargetDbset == null)
            {
                return NotFound();
            }

            var dSHSurveyFiberTarget = await _context.DSHSurveyFiberTargetDbset
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dSHSurveyFiberTarget == null)
            {
                return NotFound();
            }

            return View(dSHSurveyFiberTarget);
        }

        // GET: DSHSurveyFiberTargets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DSHSurveyFiberTargets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,UserName,Editor,DateofCreation,DateofModification,IPAddress,Region,County,Category,Nature,SectionName,Distance")] DSHSurveyFiberTarget dSHSurveyFiberTarget)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dSHSurveyFiberTarget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dSHSurveyFiberTarget);
        }

        // GET: DSHSurveyFiberTargets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DSHSurveyFiberTargetDbset == null)
            {
                return NotFound();
            }

            var dSHSurveyFiberTarget = await _context.DSHSurveyFiberTargetDbset.FindAsync(id);
            if (dSHSurveyFiberTarget == null)
            {
                return NotFound();
            }
            return View(dSHSurveyFiberTarget);
        }

        // POST: DSHSurveyFiberTargets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,UserName,Editor,DateofCreation,DateofModification,IPAddress,Region,County,Category,Nature,SectionName,Distance")] DSHSurveyFiberTarget dSHSurveyFiberTarget)
        {
            if (id != dSHSurveyFiberTarget.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dSHSurveyFiberTarget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DSHSurveyFiberTargetExists(dSHSurveyFiberTarget.Id))
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
            return View(dSHSurveyFiberTarget);
        }

        // GET: DSHSurveyFiberTargets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DSHSurveyFiberTargetDbset == null)
            {
                return NotFound();
            }

            var dSHSurveyFiberTarget = await _context.DSHSurveyFiberTargetDbset
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dSHSurveyFiberTarget == null)
            {
                return NotFound();
            }

            return View(dSHSurveyFiberTarget);
        }

        // POST: DSHSurveyFiberTargets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DSHSurveyFiberTargetDbset == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DSHSurveyFiberTargetDbset'  is null.");
            }
            var dSHSurveyFiberTarget = await _context.DSHSurveyFiberTargetDbset.FindAsync(id);
            if (dSHSurveyFiberTarget != null)
            {
                _context.DSHSurveyFiberTargetDbset.Remove(dSHSurveyFiberTarget);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DSHSurveyFiberTargetExists(int id)
        {
          return (_context.DSHSurveyFiberTargetDbset?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
