namespace _Project.Scripts.Tower
{
    public abstract class OneUnitTower : BaseTower
    {
        public OneUnitTower(TowerStats stats) : base(stats)
        {
        }

        protected override void OnPerformAction()
        {
            if (_list.Count == 0)
                return;
            
            OnPerformOnUnit(_list[0]);
        }
        protected abstract void OnPerformOnUnit(IDamageable damageable);
    }
}