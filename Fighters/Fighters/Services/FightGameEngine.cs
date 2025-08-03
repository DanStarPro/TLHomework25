using Fighters.Models.Fighters;
using Fighters.Services;

namespace Fighters.GameEngine
{
    public class FightGameEngine : IFightGameEngine
    {
        private readonly List<IFighter> _fighters = new List<IFighter>();
        private bool _battleStarted = false;

        public void AddFighter( IFighter fighter )
        {
            if ( fighter == null )
            {
                throw new ArgumentNullException( nameof( fighter ) );
            }

            if ( _battleStarted )
            {
                throw new InvalidOperationException( "Нельзя добавлять бойцов после начала битвы" );
            }

            _fighters.Add( fighter );
            Console.WriteLine( $"Боец {fighter.Name} успешно добавлен на арену!" );
        }

        public void StartBattle()
        {
            if ( _fighters.Count < 2 )
            {
                throw new InvalidOperationException( "Для битвы требуется минимум 2 бойца" );
            }

            _battleStarted = true;
            Console.WriteLine( "\n=== БИТВА НАЧАЛАСЬ ===" );

            int round = 1;
            while ( _fighters.Count( f => f.IsAlive() ) > 1 )
            {
                Console.WriteLine( $"\n--- Раунд {round++} ---" );
                FightRound();
            }

            DeclareWinner();
        }

        private void FightRound()
        {
            foreach ( var attacker in _fighters.Where( f => f.IsAlive() ).ToList() )
            {
                foreach ( var defender in _fighters.Where( f => f != attacker && f.IsAlive() ).ToList() )
                {
                    int damage = attacker.CalculateDamage();
                    int armor = defender.CalculateArmor();
                    int actualDamage = Math.Max( damage - armor, 0 );

                    defender.TakeDamage( actualDamage );

                    Console.WriteLine( $"{attacker.Name} атакует {defender.Name}" );
                    Console.WriteLine( $"Урон: {damage} | Защита: {armor} | Получено урона: {actualDamage}" );
                    Console.WriteLine( $"{defender.Name}: {defender.CurrentHealth}/{defender.MaxHealth} HP" );
                }
            }

            _fighters.RemoveAll( f => !f.IsAlive() );
        }

        private void DeclareWinner()
        {
            var winner = _fighters.FirstOrDefault( f => f.IsAlive() );
            if ( winner != null )
            {
                Console.WriteLine( $"\nПОБЕДИТЕЛЬ: {winner.Name}!" );
                Console.WriteLine( $"Оставшееся здоровье: {winner.CurrentHealth}/{winner.MaxHealth}" );
            }
            else
            {
                Console.WriteLine( "\nВсе бойцы пали в бою! Ничья!" );
            }
        }

        public void ShowFighters()
        {
            Console.WriteLine( "\nТекущие бойцы на арене:" );
            if ( _fighters.Count == 0 )
            {
                Console.WriteLine( "На арене нет бойцов" );
                return;
            }

            foreach ( var fighter in _fighters )
            {
                Console.WriteLine( fighter.GetInfo() );
                Console.WriteLine( "-------------------" );
            }
        }

        public void ClearFighters()
        {
            _fighters.Clear();
            _battleStarted = false;
            Console.WriteLine( "Арена очищена. Все бойцы удалены." );
        }

        public int FighterCount => _fighters.Count;
    }
}

