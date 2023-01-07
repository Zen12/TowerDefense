using System.Collections.Generic;

namespace _Project.Scripts.Tower
{
    public sealed class SlowAoeTower : AoeTower
    {
        public SlowAoeTower(TowerStats stats) : base(stats)
        {
        }

        protected override void OnPerformOnUnits(List<IDamageable> list)
        {
            foreach (var damageable in list)
            {
                damageable.SlowDown(_stats.Damage, _stats.Time);
            }
        }
    }
}