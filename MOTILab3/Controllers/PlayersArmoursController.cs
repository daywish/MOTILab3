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
    public class PlayersArmoursController : Controller
    {
        private readonly MOTILab3Context _context;

        public PlayersArmoursController(MOTILab3Context context)
        {
            _context = context;
        }

        // GET: PlayersArmours
        public async Task<IActionResult> Index()
        {
            return View(await _context.PlayersArmour.ToListAsync());
        }

        // GET: PlayersArmours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playersArmour = await _context.PlayersArmour
                .FirstOrDefaultAsync(m => m.ArmourId == id);
            if (playersArmour == null)
            {
                return NotFound();
            }

            return View(playersArmour);
        }

        // GET: PlayersArmours/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlayersArmours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArmourId,ArmourName,ArmourPhysicalResistance,ArmourMagicalResistance")] PlayersArmour playersArmour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(playersArmour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(playersArmour);
        }

        // GET: PlayersArmours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playersArmour = await _context.PlayersArmour.FindAsync(id);
            if (playersArmour == null)
            {
                return NotFound();
            }
            return View(playersArmour);
        }

        // POST: PlayersArmours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArmourId,ArmourName,ArmourPhysicalResistance,ArmourMagicalResistance")] PlayersArmour playersArmour)
        {
            if (id != playersArmour.ArmourId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(playersArmour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayersArmourExists(playersArmour.ArmourId))
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
            return View(playersArmour);
        }

        // GET: PlayersArmours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var playersArmour = await _context.PlayersArmour
                .FirstOrDefaultAsync(m => m.ArmourId == id);
            if (playersArmour == null)
            {
                return NotFound();
            }

            return View(playersArmour);
        }

        // POST: PlayersArmours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var playersArmour = await _context.PlayersArmour.FindAsync(id);
            List<ArmourResistanceResult> res = await _context.ArmourResistanceResult.Where(i => i.ArmourId == playersArmour.ArmourId).ToListAsync();
            foreach (var item in res)
            {
                _context.ArmourResistanceResult.Remove(item);
                await _context.SaveChangesAsync();
            }
            _context.PlayersArmour.Remove(playersArmour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayersArmourExists(int id)
        {
            return _context.PlayersArmour.Any(e => e.ArmourId == id);
        }
    }
}
