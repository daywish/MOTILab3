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
    public class EnemyWeaponsController : Controller
    {
        private readonly MOTILab3Context _context;

        public EnemyWeaponsController(MOTILab3Context context)
        {
            _context = context;
        }

        // GET: EnemyWeapons
        public async Task<IActionResult> Index()
        {
            return View(await _context.EnemyWeapon.ToListAsync());
        }

        // GET: EnemyWeapons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enemyWeapon = await _context.EnemyWeapon
                .FirstOrDefaultAsync(m => m.WeaponId == id);
            if (enemyWeapon == null)
            {
                return NotFound();
            }

            return View(enemyWeapon);
        }

        // GET: EnemyWeapons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EnemyWeapons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WeaponId,WeaponName,WeaponPhysicalDamage,WeaponMagicalDamage")] EnemyWeapon enemyWeapon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enemyWeapon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(enemyWeapon);
        }

        // GET: EnemyWeapons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enemyWeapon = await _context.EnemyWeapon.FindAsync(id);
            if (enemyWeapon == null)
            {
                return NotFound();
            }
            return View(enemyWeapon);
        }

        // POST: EnemyWeapons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WeaponId,WeaponName,WeaponPhysicalDamage,WeaponMagicalDamage")] EnemyWeapon enemyWeapon)
        {
            if (id != enemyWeapon.WeaponId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enemyWeapon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnemyWeaponExists(enemyWeapon.WeaponId))
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
            return View(enemyWeapon);
        }

        // GET: EnemyWeapons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enemyWeapon = await _context.EnemyWeapon
                .FirstOrDefaultAsync(m => m.WeaponId == id);
            if (enemyWeapon == null)
            {
                return NotFound();
            }

            return View(enemyWeapon);
        }

        // POST: EnemyWeapons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enemyWeapon = await _context.EnemyWeapon.FindAsync(id);
            List<ArmourResistanceResult> res = await _context.ArmourResistanceResult.Where(i => i.WeaponId == enemyWeapon.WeaponId).ToListAsync();
            foreach (var item in res)
            {
                _context.ArmourResistanceResult.Remove(item);
                await _context.SaveChangesAsync();
            }
            _context.EnemyWeapon.Remove(enemyWeapon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnemyWeaponExists(int id)
        {
            return _context.EnemyWeapon.Any(e => e.WeaponId == id);
        }
    }
}
