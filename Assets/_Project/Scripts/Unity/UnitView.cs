using System;
using System.Collections;
using _Project.Scripts.Movables;
using _Project.Scripts.SpawnSystems;
using _Project.Scripts.Tower;
using UnityEngine;

namespace _Project.Scripts.Unity
{
    public class UnitView : MonoBehaviour, IMovable, IDamageable
    {
        [SerializeField] private float _hp = 10;
        public bool IsAlive => _hp > 0f;
        private Transform _tr;

        private Coroutine _lastSlowRoutine;


        public float SlowDownFactor { get; private set; } = 1;

        public void TakeDamage(in float amount)
        {
            _hp -= amount;
        }

        public void SlowDown(in float time)
        {
            if (_lastSlowRoutine != null)
                StopCoroutine(_lastSlowRoutine);

            _lastSlowRoutine = StartCoroutine(SlowRoutine(time));
        }

        private IEnumerator SlowRoutine(float time)
        {
            SlowDownFactor = 0.4f;
            yield return new WaitForSeconds(time);
            SlowDownFactor = 1.0f;
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
            UpdateView();
        }

        public void UpdateView()
        {
            gameObject.SetActive(IsAlive);
        }


        private void Awake()
        {
            _tr = transform;
        }
    }
}