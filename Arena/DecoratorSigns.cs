using Arena.StatisticCreatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena.Signs
{
    public abstract class DecoratorSigns : Sign
    {
        public Sign BaseSign { get; protected set; }
    }

    public class DecoratorIgni : DecoratorSigns
    {
        public DecoratorIgni()
        {
            
            this.BaseSign = new Igni();
            this.SignName = this.BaseSign + " + exhaustion (Enchanted)";
        }

        public override void SignCast(Statistic target)
        {
            this.BaseSign.SignCast(target);
            target.Exhaustion(5);
        }
    }

    public class DecoratorAard : DecoratorSigns
    {
        public DecoratorAard()
        {

            this.BaseSign = new Aard();
            this.SignName = this.BaseSign + " + Move back enemy 5 (enchanted)";
        }

        public override void SignCast(Statistic target)
        {
            this.BaseSign.SignCast(target);
            target.Move(-5);
        }
    }

    public class DecoratorQueen : DecoratorSigns
    {
        public DecoratorQueen()
        {

            this.BaseSign = new Queen();
            this.SignName = this.BaseSign + " + additional rest 5 (enchanted)";
        }

        public override void SignCast(Statistic target)
        {
            this.BaseSign.SignCast(target);
            target.Rest(5);
        }
    }

    public class DecoratorAksii : DecoratorSigns
    {
        public DecoratorAksii()
        {

            this.BaseSign = new Aksii();
            this.SignName = this.BaseSign + " + exhaustion 40 but healing target 10 (enchanted)";
        }

        public override void SignCast(Statistic target)
        {
            this.BaseSign.SignCast(target);
            target.Exhaustion(40);
            target.Healing(10);
        }
    }
}
