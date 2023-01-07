using System;
using System.Threading;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Movables;
using _Project.Scripts.SpawnSystems;
using _Project.Scripts.Unity;
using UnityEngine;

namespace _Project.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private WaveSettings _settings;
        
        [SerializeField] private PathAdapter _path;
        [SerializeField] private UnitFabric _fabric;

        private CancellationTokenSource _token;

        private GameplayController _gamePlay;
        
        private void Awake()
        {
            _token = new CancellationTokenSource();
            _gamePlay = new GameplayController(_token.Token);
            
            var movable = new MovableController(_path, _gamePlay);
            var wave = new WaveController(_settings, _fabric, _token.Token, _gamePlay);
            
            _gamePlay.Init(movable, wave);
        }

        private void Update()
        {
            _gamePlay.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            _token.Cancel();
        }
    }
}