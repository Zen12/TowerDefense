namespace _Project.Scripts.Tower
{
    public sealed class AttackOneUnitTower : OneUnitTower
    {
        public AttackOneUnitTower(TowerStats stats) : base(stats)
        {
        }

        protected override void OnPerformOnUnit(IDamageable damageable)
        {
            damageable.TakeDamage(_stats.Damage);
        }
    }
}