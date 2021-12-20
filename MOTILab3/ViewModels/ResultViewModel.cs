using MOTILab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOTILab3.ViewModels
{
    public class ResultViewModel
    {
        public ICollection<PlayersArmour> PlayersArmours { get; set; }
        public ICollection<EnemyWeapon> EnemyWeapons { get; set; }
        public ICollection<ArmourResistanceResult> ArmourResistanceResults { get; set; }
        public PlayersArmour BestArmour { get; set; }
    }
}
