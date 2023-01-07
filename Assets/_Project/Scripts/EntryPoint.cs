using System;
using System.Collections.Generic;
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
        [SerializeField] private PlaceForTower _placeForTower;

        private CancellationTokenSource _token;


        private readonly List<IUpdatable> _updatables = new List<IUpdatable>();

        private void Awake()
        {
            _token = new CancellationTokenSource();
            var router = new RouterWaveToMovable();
            var winLose = new WinLoseChecker(20);
            var move = new MovableController(_path, winLose);
            var wave = new WaveController(_settings, _fabric, _token.Token);
            var place = new ObjectPlacer.ObjectPlacer(_placeForTower.GetBounds());
            
            
            wave.RegisterListener(router);
            wave.RegisterListener(winLose);
            
            router.Init(move);
            
            
            _updatables.Add(wave);
            _updatables.Add(move);
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            foreach (var updatable in _updatables)
            {
                updatable.Update(deltaTime);
            }
        }

        private void OnDestroy()
        {
            _token.Cancel();
        }
    }

    public interface IUpdatable
    {
        void Update(in float deltaTime);
    }
}