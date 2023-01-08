using System.Collections.Generic;

namespace _Project.Scripts.Tower
{
    public sealed class AttackAoeTower : AoeTower
    {
        public AttackAoeTower(TowerStats stats, ITowerView view) : base(stats, view)
        {
        }

        protected override void OnPerformOnUnits(List<IDamageable> list)
        {
            foreach (var damageable in list)
            {
                damageable.TakeDamage(_stats.Damage);
            }
        }
    }
}