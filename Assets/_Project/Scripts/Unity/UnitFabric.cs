using _Project.Scripts.SpawnSystems;
using UnityEngine;

namespace _Project.Scripts.Unity
{
    public class UnitFabric : MonoBehaviour, IUnitFabric
    {
        [SerializeField] private UnitView[] _unitPrefabs;
        public IUnit CreateUnit(int type)
        {
            // later some pool version
            var p = _unitPrefabs[type];
            var obj = Instantiate(p);
            return obj;
        }

        public void DestroyUnit(int type, IUnit obj)
        {
            // later some pool version
            Destroy((UnitView) obj);
        }
    }
}