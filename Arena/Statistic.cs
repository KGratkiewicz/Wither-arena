using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena.StatisticCreatures
{
    public abstract class Statistic
    {
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int Stamina { get; protected set; }
        public int MaxStamina { get; protected set; }
        public int Position { get; protected set; }
        public int Speed { get; private set; }

        public override string ToString()
        {
            string result = "";
            result += "HP: " + this.Health + "/" + this.MaxHealth + "\n";
            result += "STM: " + this.Stamina + "/" + this.MaxStamina + "\n";
            result += "Speed: " + this.Speed + "\n";
            return result;
        }

        public Statistic(int health, int stamina, int position, int speed)
        {
            this.Health = health;
            this.MaxHealth = health;
            this.Stamina = stamina;
            this.MaxStamina = stamina;
            this.Position = position;
            this.Speed = speed;
        }

        public abstract int Attack(int stamina, Statistic target);

        public void Move(bool foroward, Statistic target)
        {
            this.Stamina -= 1;
            if(foroward)
            {
                if(this.Position < target.Position)
                {
                    this.Position += this.Speed;
                    if (this.Position > target.Position)
                        this.Position = target.Position;
                }
                else
                {
                    this.Position -= this.Speed;
                    if (this.Position < target.Position)
                        this.Position = target.Position;
                }
                
            }
            else
            {
                if (this.Position < target.Position)
                {
                    this.Position -= this.Speed;
                }
                else
                {
                    this.Position += this.Speed;
                }

            }

        }

        public void Move(int distance)
        {
            this.Stamina -= 1;
            this.Position += distance;
        }

        public void Rest(int stamina)
        {
            this.Stamina += stamina;
            if (this.Stamina > this.MaxStamina)
                this.Stamina = this.MaxStamina;
        }

        public void GetDamage(int health)
        {
            this.Health -= health;
        }

        public void Exhaustion(int exhaustion)
        {
            this.Stamina -= exhaustion;
            if (this.Stamina < 0)
                this.Stamina = 0;
        }

    }

    public abstract class Oponent : Statistic
    {
        public string OponentsName { get; protected set; }

        public Oponent(string name, int health, int stamina, int position, int speed) : base(health, stamina, position, speed)
        {
            this.OponentsName = name;
        }

        public override string ToString()
        {
            string result = this.OponentsName + "\n";
            result += base.ToString();
            return result;
        }
    }
    
    public class Human : Oponent
    {
        public Weapons.Weapon EqWeapon { get; private set; }
        public Human(Weapons.Weapon weapon, string name, int health, int stamina, int position, int speed) : base(name, health, stamina, position, speed)
        {
            this.EqWeapon = weapon;
        }
        public override int Attack(int stamina, Statistic target)
        {
            if(this.Stamina < stamina)
            {
                int damage = this.EqWeapon.CountDamage(target) * stamina / 10;
                this.Stamina -= stamina;
                if (Math.Abs(target.Position - this.Position) <= this.EqWeapon.Range)
                {
                    target.GetDamage(damage);
                    return damage;
                }
            }            
            return 0;
        }

        public override string ToString()
        {
            return "Human\n" + base.ToString() + this.EqWeapon.ToString();
        }
    }
}
