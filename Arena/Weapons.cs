using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena.Weapons
{
    public abstract class Weapon
    {
        public int WeaponDamage { get; protected set; }
        public int ExtraDamage { get; protected set; }
        public int Range { get; protected set; }
        public Weapon(int weaponDamage, int extraDamage, int range)
        {
            this.WeaponDamage = weaponDamage;
            this.ExtraDamage = extraDamage;
            this.Range = range;
        }
        public abstract bool ExtraDamageTarget(StatisticCreatures.Statistic oponent);

        public override string ToString()
        {
            return "D" + this.WeaponDamage + " E" + this.ExtraDamage + " R" + this.Range;
        }

        public int CountDamage(StatisticCreatures.Statistic target)
        {
            if (this.ExtraDamageTarget(target) == true)
            {
                return this.WeaponDamage + this.ExtraDamage;
            }
            return this.WeaponDamage;
        }
    }

    public class Crossbow : Weapon
    {
        public Crossbow(int weaponDamage, int extraDamage, int range) : base(weaponDamage, extraDamage, range) { }
        public override bool ExtraDamageTarget(StatisticCreatures.Statistic oponent)
        {
            if (oponent is StatisticCreatures.FlyingMonster)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return "Crossbow: " + base.ToString();
        }
    }

    public class SteelSword : Weapon
    {
        public SteelSword(int weaponDamage, int extraDamage, int range) : base(weaponDamage, extraDamage, range) { }
        public override bool ExtraDamageTarget(StatisticCreatures.Statistic oponent)
        {
            if (oponent is StatisticCreatures.Human)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return "Steel Sword: " + base.ToString();
        }
    }

    public class SilverSword : Weapon
    {
        public SilverSword(int weaponDamage, int extraDamage, int range) : base(weaponDamage, extraDamage, range) { }
        public override bool ExtraDamageTarget(StatisticCreatures.Statistic oponent)
        {
            if (oponent is StatisticCreatures.Monster)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return "Silver Sword: " + base.ToString();
        }
    }
}
