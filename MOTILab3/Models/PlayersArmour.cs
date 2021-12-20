using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MOTILab3.Models
{
    public class PlayersArmour
    {
        public PlayersArmour()
        {
            ArmourResistanceResult = new HashSet<ArmourResistanceResult>();
        }
        [Key]
        public int ArmourId { get; set; }
        public string ArmourName { get; set; }
        public int ArmourPhysicalResistance { get; set; }
        public int ArmourMagicalResistance { get; set; }
        public virtual ICollection<ArmourResistanceResult> ArmourResistanceResult { get; set; }
    }
}
