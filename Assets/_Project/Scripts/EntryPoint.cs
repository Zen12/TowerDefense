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
        private WaveController _wave;
        private MovableController _move;
        
        private void Awake()
        {
            _token = new CancellationTokenSource();
            _gamePlay = new GameplayController();
            var winLose = new WinLoseChecker(20);
            
            _move = new MovableController(_path, winLose);
            _wave = new WaveController(_settings, _fabric, _token.Token);
            _wave.RegisterListener(_gamePlay);
            _wave.RegisterListener(winLose);
            
            _gamePlay.Init(_move);
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _move.Update(deltaTime);
            _wave.Update(deltaTime);
        }

        private void OnDestroy()
        {
            _token.Cancel();
        }
    }
}