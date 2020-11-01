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
    public class ResidenceZonesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ResidenceZonesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ResidenceZones
        public async Task<IActionResult> Index()
        {
            return View(await _context.ResidenceZones.ToListAsync());
        }

        // GET: ResidenceZones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residenceZone = await _context.ResidenceZones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (residenceZone == null)
            {
                return NotFound();
            }

            return View(residenceZone);
        }

        // GET: ResidenceZones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ResidenceZones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResidenceZone residenceZone)
        {
            if (ModelState.IsValid)
            {
                _context.Add(residenceZone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(residenceZone);
        }

        // GET: ResidenceZones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residenceZone = await _context.ResidenceZones.FindAsync(id);
            if (residenceZone == null)
            {
                return NotFound();
            }
            return View(residenceZone);
        }

        // POST: ResidenceZones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ResidenceZone residenceZone)
        {
            if (id != residenceZone.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(residenceZone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResidenceZoneExists(residenceZone.Id))
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
            return View(residenceZone);
        }

        // GET: ResidenceZones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var residenceZone = await _context.ResidenceZones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (residenceZone == null)
            {
                return NotFound();
            }

            return View(residenceZone);
        }

        // POST: ResidenceZones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var residenceZone = await _context.ResidenceZones.FindAsync(id);
            _context.ResidenceZones.Remove(residenceZone);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResidenceZoneExists(int id)
        {
            return _context.ResidenceZones.Any(e => e.Id == id);
        }


        [HttpPost]
        public async Task<JsonResult> GetBuildingByZone(int? id)
        {
            if (id == null)
            {
                return Json(false);
            }
            var buildings = await _context.ResidenceBuildings
               .Where(m => m.ResidenceZoneId == id)
               .ToListAsync();



            if (buildings == null)
            {
                return Json(false);
            }
            return Json(buildings);
        }

        [HttpPost]
        public async Task<JsonResult> GetResidenceByBuilding(int? id)
        {
            if (id == null)
            {
                return Json(false);
            }
            var residences = await _context.Residences
               .Where(m => m.ResidenceBuildingId == id && !m.IsAllotted)
               .ToListAsync();



            if (residences == null)
            {
                return Json(false);
            }
            return Json(residences);
        }

        [HttpPost]
        public async Task<JsonResult> GetResidenceById(int? id)
        {
            if (id == null)
            {
                return Json(false);
            }
            var residence = await _context.Residences
               .Where(m => m.Id == id)
               .FirstOrDefaultAsync();

            if (residence == null)
            {
                return Json(false);
            }
            return Json(residence);
        }



    }
}
