using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    class GameMenu
    {
        public StatisticCreatures.Wither PlayerWither;
        public StatisticCreatures.Oponent ChosenOponent;
        public Game.SignsOptions EnchantedSign;
        public Game game;

        public List<StatisticCreatures.Oponent> OponentsList;
        public List<Weapons.Weapon> Reinforcement;
        public List<Signs.Sign> SignMenu;

        public Weapons.SteelSword LongSteelSword;
        public Weapons.SteelSword SteelSword;
        public Weapons.SteelSword HeavySteelSword;

        public Weapons.SilverSword LongSilverSword;
        public Weapons.SilverSword SilverSword;
        public Weapons.SilverSword HeavySilverSword;

        public Weapons.Crossbow HeavyCrossbow;
        public Weapons.Crossbow SmallCrossbow;

        public Weapons.SteelSword EasyHumanSword;
        public Weapons.SteelSword MediumHumanSword;
        public Weapons.SteelSword DifficultHumanSword;

        public StatisticCreatures.Human EasyHuman;
        public StatisticCreatures.Human MediumHuman;
        public StatisticCreatures.Human DifficultHuman;

        public StatisticCreatures.Griffin YoungGriffin;
        public StatisticCreatures.Griffin Griffin;

        public StatisticCreatures.Drowner UglyDrowner;
        public StatisticCreatures.Drowner FastDrowner;
        public StatisticCreatures.Drowner FatDrowner;

        public GameMenu()
        {
            this.SignMenu = new();

            this.SignMenu.Add(new Signs.DecoratorIgni());
            this.SignMenu.Add(new Signs.DecoratorAard());
            this.SignMenu.Add(new Signs.DecoratorQueen());
            this.SignMenu.Add(new Signs.DecoratorAksii());

            this.Reinforcement = new();

            this.Reinforcement.Add(this.LongSteelSword = new(10, 5, 12));
            this.Reinforcement.Add(this.SteelSword = new(12, 5, 10));
            this.Reinforcement.Add(this.HeavySteelSword = new(20, 5, 5));

            this.Reinforcement.Add(this.LongSilverSword = new(10, 5, 12));
            this.Reinforcement.Add(this.SilverSword = new(12, 5, 10));
            this.Reinforcement.Add(this.HeavySilverSword = new(20, 5, 5));

            this.Reinforcement.Add(this.HeavyCrossbow = new(5, 5, 50));
            this.Reinforcement.Add(this.SmallCrossbow = new(5, 10, 30));

            this.PlayerWither = new("Gerald", 100, 100, 0, 10);
            this.PlayerWither.EquipWeapon(this.SteelSword);

            this.EasyHumanSword = new(10, 0, 10);
            this.MediumHumanSword = new(15, 0, 12);
            this.DifficultHumanSword = new(20, 0, 12);

            this.OponentsList = new();

            this.OponentsList.Add(this.EasyHuman = new(this.EasyHumanSword, "Easy Human", 100, 100, 50, 10));
            this.OponentsList.Add(this.MediumHuman = new(this.MediumHumanSword, "Medium Human", 120, 100, 50, 10));
            this.OponentsList.Add(this.DifficultHuman = new(this.DifficultHumanSword, "Difficult Human", 150, 100, 50, 10));

            this.OponentsList.Add(this.YoungGriffin = new("Young Griffin", 150, 200, 50, 20, 30, 5));
            this.OponentsList.Add(this.Griffin = new("Griffin", 200, 300, 50, 30, 30, 10));

            this.OponentsList.Add(this.UglyDrowner = new("Ugly Drowner", 150, 50, 50, 10, 20, 15));
            this.OponentsList.Add(this.FastDrowner = new("Fast Drowner", 100, 100, 50, 30, 10, 10));
            this.OponentsList.Add(this.FatDrowner = new("Fat Drowner", 500, 500, 50, 10, 10, 10));

            this.ChosenOponent = this.EasyHuman;
            this.EnchantedSign = Game.SignsOptions.None;
        }

        public override string ToString()
        {
            string result = "\n";
            result += "Chosen oponent: " + this.ChosenOponent.OponentsName + "\n";
            result += "Chosen weapon: " + this.PlayerWither.EqWeapon + "\n";
            result += "Enchanted sign: " + ((this.EnchantedSign != Game.SignsOptions.None)? this.SignMenu[(int)this.EnchantedSign] : " None") + "\n";
            result += "1. Create and start game \n";
            result += "2. Change oponent\n";
            result += "3. Change weapon\n";
            result += "4. Enchatn sign\n";
            result += "5. Exit\n";
            result += "Your choice: ";
            return result;
        }

        public static int ReadOption() => Convert.ToInt32(Console.ReadKey().KeyChar) - 49;
            
        public enum MainMenuOptions { NewGame, ChangeOponent, ChangeWeapon, EnchantSign, ExitGame};
        public void MainMenu()
        {

            MainMenuOptions choice;
            do
            {
                Console.Clear();
                Console.WriteLine(this);
                choice = (MainMenuOptions)ReadOption();

                switch(choice)
                {
                    case MainMenuOptions.NewGame:                        
                        this.game = new(this.PlayerWither, this.ChosenOponent, this.EnchantedSign);
                        this.game.Start();
                        this.PlayerWither.Reset(0);
                        this.ChosenOponent.Reset(50);
                        break;
                    case MainMenuOptions.ChangeOponent:
                        int chosenEnemy;
                        do
                        {
                            Console.WriteLine(this.ShowOponentList());
                            Console.WriteLine("Your choice: ");
                            chosenEnemy = ReadOption();
                            if(chosenEnemy < 0 || chosenEnemy >= this.OponentsList.Count)
                                Console.WriteLine("Incorect number of enemy! \n");                                
                        }
                        while (chosenEnemy < 0 || chosenEnemy >= this.OponentsList.Count);
                        this.ChosenOponent = this.OponentsList[chosenEnemy];
                        break;
                    case MainMenuOptions.ChangeWeapon:
                        int chosenWeapon;
                        do
                        {
                            Console.WriteLine(this.ShowReinforcement());
                            Console.WriteLine("Your choice: ");
                            chosenWeapon = ReadOption();
                            if (chosenWeapon < 0 || chosenWeapon >= this.OponentsList.Count)
                                Console.WriteLine("Incorect number of enemy! \n");                               
                        }
                        while (chosenWeapon < 0 || chosenWeapon >= this.OponentsList.Count);
                        this.PlayerWither.EquipWeapon(this.Reinforcement[chosenWeapon]);
                        break;

                    case MainMenuOptions.EnchantSign:
                        int chosenSign;
                        do
                        {
                            Console.WriteLine(this.ShowEnchantedSigns());
                            Console.WriteLine("Your choice: ");
                            chosenSign = ReadOption();
                            if (chosenSign < 0 || chosenSign >= this.SignMenu.Count)
                                Console.WriteLine("Incorect number of sign! \n");
                        }
                        while (chosenSign < 0 || chosenSign >= this.SignMenu.Count);
                        this.EnchantedSign = (Game.SignsOptions)chosenSign;
                        this.PlayerWither.EquipSign(this.SignMenu[(int)this.EnchantedSign]);
                        break;

                }

            }
            while (choice != MainMenuOptions.ExitGame);
            

        }


        public string ShowOponentList()
        {
            string result = "\n";
            for (int i=0; i<OponentsList.Count; i++)
            {
                result += (i+1).ToString() + ". " + OponentsList[i].OponentsName + "\n";
            }
            return result;
        }

        public string ShowReinforcement()
        {
            string result = "\n";
            for (int i = 0; i < Reinforcement.Count; i++)
            {
                result += (i + 1).ToString() + ". " + Reinforcement[i].ToString() + "\n";
            }
            return result;
        }

        public string ShowEnchantedSigns()
        {
            string result = "\n";
            for (int i = 0; i < SignMenu.Count; i++)
            {
                result += (i + 1).ToString() + ". " + SignMenu[i].ToString() + "\n";
            }
            return result + (SignMenu.Count + 1) + ". None \n";
        }
    }
}
