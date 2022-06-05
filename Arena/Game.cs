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
        private string LastRoundRaport;
        private int distance;

        public Game(StatisticCreatures.Wither wither, StatisticCreatures.Oponent oponent)
        {
            this.Wither = wither;
            this.Oponent = oponent;
            this.LastRoundRaport = "First round";
            this.distance = Math.Abs(this.Wither.Position - this.Oponent.Position);

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
        }
        public override string ToString()
        {
            return this.Wither + "\n" + this.Oponent + "\nDistance : " + this.distance + "\n";
        }

        public enum MenuOption { WeaponAttack, UseSign, Rest, MoveForoward, MoveBack }
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
                    default:
                        incorectOption = true;
                        Console.WriteLine("Incorect option!");
                        break;

                }
            }
            while (incorectOption);
        }

        public static string PrintPlayerMenu()
        {
            string result = "Options: \n";
            result += "1. Weapon Attack \n";
            result += "2. Use sign \n";
            result += "3. Rest \n";
            result += "4. Move foroward \n";
            result += "5. Move back \n";
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

        public void AiRound()
        {
            if (this.Oponent is StatisticCreatures.Human)
            {
                AiOption choose = this.HumanAi();

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
                }
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
