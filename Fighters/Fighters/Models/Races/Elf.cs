namespace Fighters.Models.Races
{
    public class Elf : IRace
    {
        public string Name => "Эльф";
        public int Damage => 4;
        public int Health => 80;
        public int Armor => 1;
    }
}
