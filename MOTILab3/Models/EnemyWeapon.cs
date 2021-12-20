using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MOTILab3.Models
{
    public class EnemyWeapon
    {
        public EnemyWeapon()
        {
            ArmourResistanceResult = new HashSet<ArmourResistanceResult>();
        }
        [Key]
        public int WeaponId { get; set; }
        public string WeaponName { get; set; }
        public int WeaponPhysicalDamage { get; set; }
        public int WeaponMagicalDamage { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<ArmourResistanceResult> ArmourResistanceResult { get; set; }
    }
}
