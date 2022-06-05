using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena.StatisticCreatures
{
    public abstract class Monster : Oponent
    {
        public bool Balance { get; protected set; }
        public int Strength { get; protected set; }
        public int AttackRange { get; protected set; }

        public Monster(string name, int health, int stamina, int position, int speed, int strength, int attackRange) : base(name, health, stamina, position, speed) 
        {
            this.Strength = strength;
            this.Balance = true;
            this.AttackRange = attackRange;
        }

        public override string ToString()
        {
            string result = "Monster\n" + base.ToString();
            result += (this.Balance)? "Balance: Stand\n" : "Balance: Overturned \n";
            result += "Strength: " + this.Strength + "(R" + this.AttackRange + "\n";
            return result;
        }

        public void GetUp(int stamina)
        {
            if (this.Stamina >= stamina)
            {
                this.Stamina -= stamina;
                this.Balance = true;
            }
        }

        public virtual void TipOver()
        {
            this.Balance = false;
        }
    }

    public abstract class FlyingMonster : Monster
    {
        public bool InFlight { get; protected set; }

        public FlyingMonster(string name, int health, int stamina, int position, int speed, int strength, int attackRange) : base(name, health, stamina, position, speed, strength, attackRange) 
        {
            this.InFlight = true;
        }

        public void Land()
        {
            this.InFlight = false;
        }

        public override void TipOver()
        {
            this.InFlight = false;
            this.Balance = false;
        }

        public override string ToString()
        {
            if(this.InFlight)
                return base.ToString() + "On the fly \n";
            return base.ToString() + "On the ground \n";
        }
    }

    public class Griffin : FlyingMonster
    {
        public Griffin(string name, int health, int stamina, int position, int speed, int strength, int attackRange) : base(name, health, stamina, position, speed, strength, attackRange) { }
        public override int Attack(int stamina, Statistic target)
        {
            if (this.Balance == true)
            {
                if (this.Stamina >= stamina)
                {
                    this.Stamina -= stamina;
                    if (this.InFlight)
                    {
                        target.GetDamage(this.Strength);
                        return this.Strength;
                    }
                    else
                    {
                        target.GetDamage(this.Strength / 2);
                        return this.Strength / 2;
                    }
                }
            }
            return 0;
            
        }
    }

    public class Drowner : Monster
    {
        public Drowner(string name, int health, int stamina, int position, int speed, int strength, int attackRange) : base(name, health, stamina, position, speed, strength, attackRange) { }

        public override int Attack(int stamina, Statistic target)
        {
            this.Stamina -= stamina;
            return 0;
        }
    }
}
