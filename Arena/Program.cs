using System;

namespace Arena
{
    class Program
    {
        static void Main()
        {
            StatisticCreatures.Wither wither = new("Gerald", 100, 100, 0, 10);
            var oponent = new Arena.StatisticCreatures.Human(new Arena.Weapons.SteelSword(10, 10, 10), "Bandit", 100, 100, 15, 10);
            wither.EquipWeapon(new Weapons.Crossbow(10, 5, 30));
            wither.EquipSign(new Signs.Aard());
            Game game = new(wither, oponent);
        }
    }
}
