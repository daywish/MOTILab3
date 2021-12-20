using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MOTILab3.Data;
using MOTILab3.Models;

namespace MOTILab3.Controllers
{
    public class ArmourResistanceResultsController : Controller
    {
        private readonly MOTILab3Context _context;

        public ArmourResistanceResultsController(MOTILab3Context context)
        {
            _context = context;
        }

        // GET: ArmourResistanceResults
        public async Task<IActionResult> Index()
        {
            return View(await _context.ArmourResistanceResult.ToListAsync());
        }

        // GET: ArmourResistanceResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armourResistanceResult = await _context.ArmourResistanceResult
                .FirstOrDefaultAsync(m => m.ResultId == id);
            if (armourResistanceResult == null)
            {
                return NotFound();
            }

            return View(armourResistanceResult);
        }

        // GET: ArmourResistanceResults/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ArmourResistanceResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResultId,ArmourId,WeaponId,ResistedDamage")] ArmourResistanceResult armourResistanceResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(armourResistanceResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(armourResistanceResult);
        }

        // GET: ArmourResistanceResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armourResistanceResult = await _context.ArmourResistanceResult.FindAsync(id);
            if (armourResistanceResult == null)
            {
                return NotFound();
            }
            return View(armourResistanceResult);
        }

        // POST: ArmourResistanceResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ResultId,ArmourId,WeaponId,ResistedDamage")] ArmourResistanceResult armourResistanceResult)
        {
            if (id != armourResistanceResult.ResultId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(armourResistanceResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArmourResistanceResultExists(armourResistanceResult.ResultId))
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
            return View(armourResistanceResult);
        }

        // GET: ArmourResistanceResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var armourResistanceResult = await _context.ArmourResistanceResult
                .FirstOrDefaultAsync(m => m.ResultId == id);
            if (armourResistanceResult == null)
            {
                return NotFound();
            }

            return View(armourResistanceResult);
        }

        // POST: ArmourResistanceResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var armourResistanceResult = await _context.ArmourResistanceResult.FindAsync(id);
            _context.ArmourResistanceResult.Remove(armourResistanceResult);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArmourResistanceResultExists(int id)
        {
            return _context.ArmourResistanceResult.Any(e => e.ResultId == id);
        }
    }
}
