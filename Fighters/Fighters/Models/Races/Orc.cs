namespace Fighters.Models.Races
{
    public class Orc : IRace
    {
        public string Name => "Орк";
        public int Damage => 2;
        public int Health => 120;
        public int Armor => 3;
    }
}
