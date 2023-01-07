using _Project.Scripts.Movables;
using _Project.Scripts.SpawnSystems;
using UnityEngine;

namespace _Project.Scripts.Unity
{
    public class UnitView : MonoBehaviour, IUnit, IMovable
    {
        public bool IsAlive { get; private set; }

        public Vector3 Position
        {
            get => _tr.position;
            set => _tr.position = value;
        }

        public Quaternion Rotation
        {
            get => _tr.rotation;
            set => _tr.rotation = value;
        }

        private Transform _tr;

        private void Awake()
        {
            _tr = transform;
        }
    }
}