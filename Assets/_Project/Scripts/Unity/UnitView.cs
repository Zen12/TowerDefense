using System;
using _Project.Scripts.Movables;
using _Project.Scripts.SpawnSystems;
using _Project.Scripts.Tower;
using UnityEngine;

namespace _Project.Scripts.Unity
{
    public class UnitView : MonoBehaviour, IUnit, IMovable, IDamageable
    {
        [SerializeField] private float _hp = 10;
        public bool IsAlive => _hp > 0f;
        private Transform _tr;


        public void TakeDamage(in float amount)
        {
            _hp -= amount;
        }

        public void SlowDown(in float time)
        {
        }

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

        public void OnFinishPath()
        {
            _hp = 0;
            gameObject.SetActive(false);
        }


        private void Awake()
        {
            _tr = transform;
        }
    }
}