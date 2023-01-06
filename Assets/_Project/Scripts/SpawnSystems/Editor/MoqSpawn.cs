using System.Collections.Generic;

namespace _Project.Scripts.SpawnSystems.Editor
{
    internal class DummyFabric : IUnitFabric
    {
        public List<DummyUnit> Units = new List<DummyUnit>();
        public IUnit CreateUnit(int type)
        {
            var u = new DummyUnit();
            Units.Add(u);
            return u;
        }

        public void DestroyUnit(int type, IUnit obj)
        {
            Units.Remove((DummyUnit)obj);
        }
    }

    internal class DummyUnit : IUnit
    {
        public bool IsAlive { get; set; } = false;
    }


}