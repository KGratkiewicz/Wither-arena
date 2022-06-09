using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    public class Game
    {
        private readonly StatisticCreatures.Wither Wither;
        private readonly StatisticCreatures.Oponent Oponent;
        private readonly SignsOptions EnchantedSign;
        private string LastRoundRaport;
        private int distance;

        public Game(StatisticCreatures.Wither wither, StatisticCreatures.Oponent oponent, SignsOptions enchantedSign)
        {
            this.Wither = wither;
            this.Oponent = oponent;
            this.EnchantedSign = enchantedSign;
            this.LastRoundRaport = "First round";
            this.distance = Math.Abs(this.Wither.Position - this.Oponent.Position);
        }

        public void Start()
        {
            GameStatus result;
            while ((result = this.GameRound()) == GameStatus.InProgress) ;

            switch (result)
            {
                case GameStatus.Draw:
                    Console.WriteLine("\nGame was a draw");
                    break;
                case GameStatus.Win:
                    Console.WriteLine("\nYou won this game!");
                    break;
                case GameStatus.Lose:
                    Console.WriteLine("\nYou lose this game!");
                    break;
            }
            ReadOption();
        }
        public override string ToString()
        {
            return this.Wither + "\n" + this.Oponent + "\nDistance : " + this.distance + "\n";
        }

        public enum MenuOption { WeaponAttack, UseSign, Rest, MoveForoward, MoveBack, ChangeSign}
        public enum SignsOptions {Igni, Aard, Queen, Aksii, None}
        public enum AiOption { Attack, Rest, MoveForoward, MoveBack, GetUp }
        public enum GameStatus { InProgress, Win, Lose, Draw }
        public void PlayerRound()
        {
            bool incorectOption;
            do
            {
                incorectOption = false;
                PrintPlayerMenu();
                MenuOption choose = (MenuOption)ReadOption();

                switch (choose)
                {
                    case MenuOption.WeaponAttack:
                        int damage = this.Wither.Attack(10, this.Oponent);
                        this.LastRoundRaport += "Wither hit oponent by weapon: " + damage + "\n";
                        break;
                    case MenuOption.UseSign:
                        this.Wither.CastSign(this.Oponent);
                        this.LastRoundRaport += "Wither used sign: " + this.Wither.EqSign + "\n";
                        break;
                    case MenuOption.Rest:
                        this.Wither.Rest(20);
                        this.LastRoundRaport += "Wither rest his stamina by: 20\n";
                        break;
                    case MenuOption.MoveForoward:
                        this.Wither.Move(true, this.Oponent);
                        this.LastRoundRaport += "Wither moved foroward enemy\n";
                        break;
                    case MenuOption.MoveBack:
                        this.Wither.Move(false, this.Oponent);
                        this.LastRoundRaport += "Wither moved back enemy\n";
                        break;
                    case MenuOption.ChangeSign:
                        ChangeSign();
                        this.LastRoundRaport += "Wither changed sign\n";
                        break;
                    default:
                        incorectOption = true;
                        Console.WriteLine("\nIncorect option!");
                        break;

                }
            }
            while (incorectOption);
        }
        
        public void ChangeSign()
        {
            Console.WriteLine("\n1. Igni");
            Console.WriteLine("2. Aard");
            Console.WriteLine("3. Queen");
            Console.WriteLine("4. Aksii");
            Console.WriteLine("Your choose: ");
            SignsOptions newSign = (SignsOptions)ReadOption();
            
            switch(newSign)
            {
                case SignsOptions.Igni:
                    if(this.EnchantedSign == newSign)
                    {
                        this.Wither.EquipSign(new Signs.DecoratorIgni());
                    }
                    else
                    {
                        this.Wither.EquipSign(new Signs.Igni());
                    }
                    break;
                case SignsOptions.Aard:
                    if (this.EnchantedSign == newSign)
                    {
                        this.Wither.EquipSign(new Signs.DecoratorAard());
                    }
                    else
                    {
                        this.Wither.EquipSign(new Signs.Aard());
                    }
                    break;
                case SignsOptions.Queen:
                    if (this.EnchantedSign == newSign)
                    {
                        this.Wither.EquipSign(new Signs.DecoratorQueen());
                    }
                    else
                    {
                        this.Wither.EquipSign(new Signs.Aard());
                    }
                    break;
                case SignsOptions.Aksii:
                    if (this.EnchantedSign == newSign)
                    {
                        this.Wither.EquipSign(new Signs.DecoratorAksii());
                    }
                    else
                    {
                        this.Wither.EquipSign(new Signs.Aksii());
                    }
                    break;
            }

        }

        public static string PrintPlayerMenu()
        {
            string result = "Options: \n";
            result += "1. Weapon Attack \n";
            result += "2. Use sign \n";
            result += "3. Rest \n";
            result += "4. Move foroward \n";
            result += "5. Move back \n";
            result += "6. Change sign\n";
            result += "Your choose: ";
            Console.WriteLine(result);
            return result;
        }
        public static int ReadOption() => Convert.ToInt32(Console.ReadKey().KeyChar) - 49;

        private AiOption HumanAi()
        {
            var human = this.Oponent as StatisticCreatures.Human;
            if (human.Stamina < 10)
                return AiOption.Rest;
            if(this.distance <= human.EqWeapon.Range)
                return AiOption.Attack;
            return AiOption.MoveForoward;
                
        }

        private AiOption MonsterAi()
        {
            var monster = this.Oponent as StatisticCreatures.Monster;
            if (monster.Stamina < 10)
                return AiOption.Rest;
            if (monster.Balance != true)
                return AiOption.GetUp;
            if (this.distance <= monster.AttackRange)
                return AiOption.Attack;
            return AiOption.MoveForoward;


        }

        public void AiRound()
        {
            AiOption choose = AiOption.Rest;
            if (this.Oponent is StatisticCreatures.Human)
            {
                choose = this.HumanAi();
            }

            if (this.Oponent is StatisticCreatures.Monster)
            {
                choose = this.MonsterAi();
            }

            switch (choose)
            {
                case AiOption.Attack:
                    int damage = this.Oponent.Attack(10, this.Wither);
                    this.LastRoundRaport += "Oponent hit wither by weapon: " + damage + "\n";
                    break;
                case AiOption.Rest:
                    this.Oponent.Rest(20);
                    this.LastRoundRaport += "Oponent rest his stamina by: 20\n";
                    break;
                case AiOption.MoveForoward:
                    this.Oponent.Move(true, this.Wither);
                    this.LastRoundRaport += "Opnent moved foroward wither\n";
                    break;
                case AiOption.GetUp:
                    var monster = this.Oponent as StatisticCreatures.Monster;
                    monster.GetUp(2);
                    this.LastRoundRaport += "Opnent get up\n";
                    break;
            }

        }

        public GameStatus GameRound()
        {
            Console.Clear();            
            Console.WriteLine(this.LastRoundRaport);
            this.LastRoundRaport = "";
            Console.WriteLine(this);
            this.PlayerRound();
            this.distance = Math.Abs(this.Wither.Position - this.Oponent.Position);
            this.AiRound();
            this.distance = Math.Abs(this.Wither.Position - this.Oponent.Position);

            if (this.Wither.Health <= 0 && this.Oponent.Health <= 0)
                return GameStatus.Draw;
            if (this.Wither.Health <= 0)
                return GameStatus.Lose;
            if (this.Oponent.Health <= 0)
                return GameStatus.Win;
            return GameStatus.InProgress;
        }

    }
}
