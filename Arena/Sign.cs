using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena.Signs
{
    public abstract class Sign
    {
        public string SignName { get; protected set; }
        public abstract void SignCast(StatisticCreatures.Statistic target);

        public override string ToString()
        {
            return this.SignName;
        }
    }

    public class Igni : Sign
    {
        public Igni()
        {
            this.SignName = "Igni: 10 fire damage.";
        }
        public override void SignCast(StatisticCreatures.Statistic target)
        {
            target.GetDamage(10);
        }
    }

    public class Aard : Sign
    {
        public Aard()
        {
            this.SignName = "Aard: Overturn monster or hit and move human";
        }
        public override void SignCast(StatisticCreatures.Statistic target)
        {
            
            if(target is StatisticCreatures.Monster)
            {
                StatisticCreatures.Monster tmpTarget = target as StatisticCreatures.Monster;
                tmpTarget.TipOver();
            }
            else
            {
                target.Move(5);
                target.GetDamage(5);
            }
        }
    }

    public class Queen : Sign
    {
        public Queen()
        {
            this.SignName = "Queen: Health yourself";
        }
        public override void SignCast(StatisticCreatures.Statistic target)
        {
            target.Healing(10);
        }
    }

    public class Aksii : Sign
    {
        public Aksii()
        {
            this.SignName = "Aksii: Distrupt flying creature and exhaus target.";
        }
        public override void SignCast(StatisticCreatures.Statistic target)
        {
            if (target is StatisticCreatures.FlyingMonster)
            {
                StatisticCreatures.FlyingMonster tmpTarget = target as StatisticCreatures.FlyingMonster;
                tmpTarget.Land();
                tmpTarget.Exhaustion(5);
            }
            else
            {
                target.Exhaustion(10);
            }
        }
    }
}
