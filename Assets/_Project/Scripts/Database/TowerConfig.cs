using _Project.Scripts.Unity;
using UnityEngine;

namespace _Project.Scripts.Database
{
    [CreateAssetMenu(fileName = "TowerConfig", menuName = "_Config/TowerConfig", order = 1)]
    public sealed class TowerConfig : ScriptableObject
    {
        public TowerItem[] Items;
    }

    [System.Serializable]
    public sealed class TowerItem
    {
        public TowerView Tower;
        public Sprite Preview;
    }
}
