using System.Collections.Generic;

namespace _Project.Scripts.Tower
{
    public sealed class SlowAoeTower : AoeTower
    {
        public SlowAoeTower(TowerStats stats, ITowerView view) : base(stats, view)
        {
        }

        protected override void OnPerformOnUnits(List<IDamageable> list)
        {
            foreach (var damageable in list)
            {
                damageable.SlowDown(_stats.SlowTime);
            }
        }
    }
}