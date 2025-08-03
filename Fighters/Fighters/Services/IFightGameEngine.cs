using Fighters.Models.Fighters;

namespace Fighters.GameEngine
{
    public interface IFightGameEngine
    {
        void StartBattle();
        void ShowFighters();
        void AddFighter( IFighter fighter );
        void ClearFighters();
        int FighterCount { get; }
    }
}
