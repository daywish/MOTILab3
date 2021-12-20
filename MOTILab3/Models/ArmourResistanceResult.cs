using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MOTILab3.Models
{
    public class ArmourResistanceResult
    {
        [Key]
        public int ResultId { get; set; }
        public int ArmourId { get; set; }
        public int WeaponId { get; set; }
        public int ResistedDamage { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual PlayersArmour PlayersArmour { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual EnemyWeapon EnemyWeapon { get; set; }
    }
}
