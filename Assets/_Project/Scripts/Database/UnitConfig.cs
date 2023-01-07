using _Project.Scripts.Unity;
using UnityEngine;

namespace _Project.Scripts.Database
{
    [CreateAssetMenu(fileName = "UnitConfig", menuName = "_Config/UnitConfig", order = 1)]
    public sealed class UnitConfig : ScriptableObject
    {
        public UnitView[] Prefabs;
    }
}
