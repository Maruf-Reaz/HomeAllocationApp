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
    public class ResidencesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResidencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Residences
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Residences.Include(r => r.ResidenceBuilding);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Residences/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residence = await _context.Residences
                .Include(r => r.ResidenceBuilding)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (residence == null)
            {
                return NotFound();
            }

            return View(residence);
        }

        // GET: Residences/Create
        public IActionResult Create()
        {
            ViewData["ResidenceBuildingId"] = new SelectList(_context.ResidenceBuildings, "Id", "Name");
            return View();
        }

        // POST: Residences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Residence residence)
        {
            if (ModelState.IsValid)
            {
                _context.Add(residence);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ResidenceBuildingId"] = new SelectList(_context.ResidenceBuildings, "Id", "Name", residence.ResidenceBuildingId);
            return View(residence);
        }

        // GET: Residences/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residence = await _context.Residences.FindAsync(id);
            if (residence == null)
            {
                return NotFound();
            }
            ViewData["ResidenceBuildingId"] = new SelectList(_context.ResidenceBuildings, "Id", "Name", residence.ResidenceBuildingId);
            return View(residence);
        }

        // POST: Residences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Residence residence)
        {
            if (id != residence.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(residence);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResidenceExists(residence.Id))
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
            ViewData["ResidenceBuildingId"] = new SelectList(_context.ResidenceBuildings, "Id", "Name", residence.ResidenceBuildingId);
            return View(residence);
        }

        // GET: Residences/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residence = await _context.Residences
                .Include(r => r.ResidenceBuilding)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (residence == null)
            {
                return NotFound();
            }

            return View(residence);
        }

        // POST: Residences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var residence = await _context.Residences.FindAsync(id);
            _context.Residences.Remove(residence);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResidenceExists(int id)
        {
            return _context.Residences.Any(e => e.Id == id);
        }
    }
}
