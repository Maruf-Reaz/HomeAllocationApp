using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BNHomeAllocation.Data;
using BnHomeAllocationApp.Models;

namespace BnHomeAllocationApp.Controllers
{
    public class OfficerRanksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OfficerRanksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: OfficerRanks
        public async Task<IActionResult> Index()
        {
            return View(await _context.OfficerRanks.ToListAsync());
        }

        // GET: OfficerRanks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officerRank = await _context.OfficerRanks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (officerRank == null)
            {
                return NotFound();
            }

            return View(officerRank);
        }

        // GET: OfficerRanks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OfficerRanks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( OfficerRank officerRank)
        {
            if (ModelState.IsValid)
            {
                _context.Add(officerRank);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(officerRank);
        }

        // GET: OfficerRanks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officerRank = await _context.OfficerRanks.FindAsync(id);
            if (officerRank == null)
            {
                return NotFound();
            }
            return View(officerRank);
        }

        // POST: OfficerRanks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OfficerRank officerRank)
        {
            if (id != officerRank.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(officerRank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfficerRankExists(officerRank.Id))
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
            return View(officerRank);
        }

        // GET: OfficerRanks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var officerRank = await _context.OfficerRanks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (officerRank == null)
            {
                return NotFound();
            }

            return View(officerRank);
        }

        // POST: OfficerRanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var officerRank = await _context.OfficerRanks.FindAsync(id);
            _context.OfficerRanks.Remove(officerRank);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfficerRankExists(int id)
        {
            return _context.OfficerRanks.Any(e => e.Id == id);


        }

        [HttpPost]
        public async Task<JsonResult> GetOfficersByRank(int? id)
        {
            if (id == null)
            {
                return Json(false);
            }
            var officers = await _context.Officers
               .Where(m => m.OfficerRankId == id)
               .ToListAsync();



            if (officers == null)
            {
                return Json(false);
            }
            return Json(officers);
        }









    }
}
