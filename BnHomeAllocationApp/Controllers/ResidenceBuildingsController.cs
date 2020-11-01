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
    public class ResidenceBuildingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResidenceBuildingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResidenceBuildings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ResidenceBuildings.Include(r => r.BuildingType).Include(r => r.ResidenceZone);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ResidenceBuildings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residenceBuilding = await _context.ResidenceBuildings
                .Include(r => r.BuildingType)
                .Include(r => r.ResidenceZone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (residenceBuilding == null)
            {
                return NotFound();
            }

            return View(residenceBuilding);
        }

        // GET: ResidenceBuildings/Create
        public IActionResult Create()
        {
            ViewData["BuildingTypeId"] = new SelectList(_context.BuildingTypes, "Id", "Name");
            ViewData["ResidenceZoneId"] = new SelectList(_context.ResidenceZones, "Id", "Name");
            return View();
        }

        // POST: ResidenceBuildings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResidenceBuilding residenceBuilding)
        {
            if (ModelState.IsValid)
            {
                _context.Add(residenceBuilding);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingTypeId"] = new SelectList(_context.BuildingTypes, "Id", "Name", residenceBuilding.BuildingTypeId);
            ViewData["ResidenceZoneId"] = new SelectList(_context.ResidenceZones, "Id", "Name", residenceBuilding.ResidenceZoneId);
            return View(residenceBuilding);
        }

        // GET: ResidenceBuildings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residenceBuilding = await _context.ResidenceBuildings.FindAsync(id);
            if (residenceBuilding == null)
            {
                return NotFound();
            }
            ViewData["BuildingTypeId"] = new SelectList(_context.BuildingTypes, "Id", "Name", residenceBuilding.BuildingTypeId);
            ViewData["ResidenceZoneId"] = new SelectList(_context.ResidenceZones, "Id", "Name", residenceBuilding.ResidenceZoneId);
            return View(residenceBuilding);
        }

        // POST: ResidenceBuildings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResidenceBuilding residenceBuilding)
        {
            if (id != residenceBuilding.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(residenceBuilding);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResidenceBuildingExists(residenceBuilding.Id))
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
            ViewData["BuildingTypeId"] = new SelectList(_context.BuildingTypes, "Id", "Name", residenceBuilding.BuildingTypeId);
            ViewData["ResidenceZoneId"] = new SelectList(_context.ResidenceZones, "Id", "Name", residenceBuilding.ResidenceZoneId);
            return View(residenceBuilding);
        }

        // GET: ResidenceBuildings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residenceBuilding = await _context.ResidenceBuildings
                .Include(r => r.BuildingType)
                .Include(r => r.ResidenceZone)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (residenceBuilding == null)
            {
                return NotFound();
            }

            return View(residenceBuilding);
        }

        // POST: ResidenceBuildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var residenceBuilding = await _context.ResidenceBuildings.FindAsync(id);
            _context.ResidenceBuildings.Remove(residenceBuilding);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResidenceBuildingExists(int id)
        {
            return _context.ResidenceBuildings.Any(e => e.Id == id);
        }
    }
}
