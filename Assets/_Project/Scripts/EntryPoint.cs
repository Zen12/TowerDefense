using System;
using System.Threading;
using _Project.Scripts.Movables;
using _Project.Scripts.SpawnSystems;
using _Project.Scripts.Unity;
using UnityEngine;

namespace _Project.Scripts
{
    public class EntryPoint : MonoBehaviour, IMoveControllerListener
    {
        [SerializeField] private WaveSettings _settings;
        
        [SerializeField] private PathAdapter _path;
        [SerializeField] private UnitFabric _fabric;

        private MovableController _movable;
        private WaveController _wave;

        private CancellationTokenSource _token;
        
        private void Awake()
        {
            _token = new CancellationTokenSource();
            _movable = new MovableController(_path, this);
            _wave = new WaveController(_settings, _fabric, _token.Token);
        }

        private void Update()
        {
            _movable.Update(Time.deltaTime);
            _wave.Update();
        }

        public void OnFinishMovable(IMovable movable)
        {
            
        }

        private void OnDestroy()
        {
            _token.Cancel();
        }
    }
}