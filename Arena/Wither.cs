using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena.StatisticCreatures
{
    public class Wither : Statistic
    {
        public string Name { get; private set; }
        public Weapons.Weapon EqWeapon { get; private set; }
        public Signs.Sign EqSign { get; private set; }

        public Wither(string name, int health, int stamina, int position, int speed) : base(health, stamina, position, speed)
        {
            this.Name = name;
        }

        public void EquipWeapon(Weapons.Weapon weapon)
        {
            this.EqWeapon = weapon;
        }

        public void EquipSign(Signs.Sign sign)
        {
            this.EqSign = sign;
        }

        public override int Attack(int stamina, Statistic target)
        {
            if (this.Stamina > stamina)
            {
                int damage = this.EqWeapon.CountDamage(target) * this.Stamina / 100;
                this.Stamina -= stamina;
                if (Math.Abs(target.Position - this.Position) <= this.EqWeapon.Range)
                {
                    target.GetDamage(damage);
                    return damage;
                }                
            }            
            return 0;

        }

        public void CastSign(Statistic target)
        {
            this.EqSign.SignCast(target);
        }

        public override string ToString()
        {
            return this.Name + "\n" +base.ToString() + "Equipmet weapon: " + this.EqWeapon + "\nSign: " + this.EqSign + "\n";
        }
    }
}
