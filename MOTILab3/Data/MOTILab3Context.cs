using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MOTILab3.Models;

namespace MOTILab3.Data
{
    public class MOTILab3Context : DbContext
    {
        public MOTILab3Context (DbContextOptions<MOTILab3Context> options)
            : base(options)
        {
        }

        public DbSet<MOTILab3.Models.PlayersArmour> PlayersArmour { get; set; }

        public DbSet<MOTILab3.Models.EnemyWeapon> EnemyWeapon { get; set; }

        public DbSet<MOTILab3.Models.ArmourResistanceResult> ArmourResistanceResult { get; set; }
    }
}
