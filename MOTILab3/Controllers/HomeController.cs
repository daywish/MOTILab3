using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOTILab3.Data;
using MOTILab3.Models;
using MOTILab3.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MOTILab3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private MOTILab3Context _context;

        public HomeController(ILogger<HomeController> logger, MOTILab3Context context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<PlayersArmour> armours = _context.PlayersArmour.ToList();
            List<EnemyWeapon> weapons = _context.EnemyWeapon.ToList();
            List<ArmourResistanceResult> res = _context.ArmourResistanceResult.ToList();
            ResultViewModel model = new ResultViewModel();
            model.PlayersArmours = armours;
            model.EnemyWeapons = weapons;
            model.ArmourResistanceResults = res;
            return View(model);
        }

        public async Task<IActionResult> GenerateResist()
        {
            if(_context.ArmourResistanceResult.Any())
            {
                List<ArmourResistanceResult> res = _context.ArmourResistanceResult.ToList();
                foreach(var item in res)
                {
                    _context.ArmourResistanceResult.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }
            List<PlayersArmour> armours = _context.PlayersArmour.ToList();
            List<EnemyWeapon> weapons = _context.EnemyWeapon.ToList();
            List<ArmourResistanceResult> resists = new List<ArmourResistanceResult>();
            foreach(var armour in armours)
            {
                foreach(var weapon in weapons)
                {
                    resists.Add(new ArmourResistanceResult()
                    {
                        ArmourId = armour.ArmourId,
                        WeaponId = weapon.WeaponId,
                        ResistedDamage = (weapon.WeaponPhysicalDamage*armour.ArmourPhysicalResistance/100+weapon.WeaponMagicalDamage*armour.ArmourMagicalResistance/100),
                        PlayersArmour = armour,
                        EnemyWeapon = weapon
                    });
                }
            }
            foreach(var item in resists)
            {
                _context.ArmourResistanceResult.Add(item);
                await _context.SaveChangesAsync();
            }
            List<ArmourResistanceResult> result = _context.ArmourResistanceResult.ToList();
            Dictionary<PlayersArmour, int> minMax = new Dictionary<PlayersArmour, int>();
            foreach(var armour in armours)
            {
                int min = result.Where(i => i.ArmourId == armour.ArmourId).First().ResistedDamage;
                foreach(var damage in result)
                {
                    if(damage.ArmourId==armour.ArmourId && min>damage.ResistedDamage)
                    {
                        min = damage.ResistedDamage;
                    }
                }
                minMax.Add(armour, min);
            }
            PlayersArmour maxVal = new PlayersArmour();
            int max = minMax.First().Value;
            int count = 0;
            List<PlayersArmour> best = new List<PlayersArmour>();
            foreach(var item in minMax)
            {
                if (item.Value > max)
                {
                    max = item.Value;
                }
            }
            foreach(var item in minMax)
            {
                if(item.Value==max)
                {
                    count++;
                    best.Add(item.Key);
                }
            }
            ResultViewModel model = new ResultViewModel();
            model.PlayersArmours = armours;
            model.EnemyWeapons = weapons;
            model.ArmourResistanceResults = result;
            if (count==1)
            {
                model.BestArmour = best.First();
            }
            else
            {
                int i = 0;
                int[] avgResistace = new int[best.Count];
                foreach(var item in best)
                {
                    List<ArmourResistanceResult> results = _context.ArmourResistanceResult.Where(i => i.ArmourId == item.ArmourId).ToList();
                    foreach(var dmg in results)
                    {
                        avgResistace[i] += dmg.ResistedDamage;
                    }
                    i++;
                }
                int maxInd = 0;
                int maxRes = 0;
                for(int j =0; j<avgResistace.Length;j++)
                {
                    if(avgResistace[j]>maxRes)
                    {
                        maxRes = avgResistace[j];
                        maxInd = j;
                    }
                }
                model.BestArmour = best[maxInd];
            }
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
