using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Tower
{
    public abstract class BaseTower : IUpdatable
    {
        protected readonly List<IDamageable> _list = new List<IDamageable>();
        protected readonly TowerStats _stats;

        private float _currentTime;

        protected BaseTower(TowerStats stats)
        {
            _stats = stats;
            _currentTime = stats.Delay + float.Epsilon; // immediate attack 
        }


        public void Add(IDamageable damageable) => _list.Add(damageable);

        public void Remove(IDamageable damageable) => _list.Remove(damageable);


        public void Update(in float deltaTime)
        {
            _currentTime += deltaTime;
            if (_currentTime >= _stats.Delay)
            {
                if (_list.Count > 0)
                {
                    _currentTime = 0;
                }
                else
                {
                    return;
                }

                OnPerformAction();
            }
        }

        protected abstract void OnPerformAction();
    }

    public sealed class TowerStats
    {
        public float Damage;
        public float Time;
        public float Delay;
        public float AoeRange;
    }

    public interface IDamageable
    {
        void TakeDamage(in float amount);

        void SlowDown(in float amount, in float time);
        Vector3 Position { get; }
    }
}
