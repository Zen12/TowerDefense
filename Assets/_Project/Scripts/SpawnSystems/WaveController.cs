using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace _Project.Scripts.SpawnSystems
{

    public sealed class WaveController
    {
        public int CurrentWave { get; private set; }
        public bool IsFinishAllWaves { get; private set; }
        public bool IsSpawning { get; private set; }

        private readonly WaveSettings _settings;
        private readonly IUnitFabric _fabric;

        private readonly List<IUnit> _list = new List<IUnit>();


        private CancellationToken _token;

        public WaveController(WaveSettings settings, IUnitFabric fabric, CancellationToken token)
        {
            _settings = settings;
            _fabric = fabric;
            _token = token;
            CurrentWave = -1; //not started
        }

        public void Update()
        {
            if (IsSpawning)
                return;
            
            if (IsAtLeastOneAlive())
                return;
            
            // clear all previous units
            if (CurrentWave >= 0)
            {
                var wave = _settings.Waves[CurrentWave];

                foreach (var unit in _list)
                {
                    _fabric.DestroyUnit(wave.Type, unit);
                }
                _list.Clear();
            }

            IsSpawning = true;
            _ = SpawnAsync();
        }

        private async Task SpawnAsync()
        {
            CurrentWave++;
            if (CurrentWave >= _settings.Waves.Count)
            {
                IsFinishAllWaves = true;
                IsSpawning = false;
                return;
            }

            var wave = _settings.Waves[CurrentWave];

            for (int i = 0; i < wave.Amount; i++)
            {
                var obj = _fabric.CreateUnit(wave.Type);
                _list.Add(obj);
                await Task.Delay(TimeSpan.FromSeconds(wave.Delay));
                if (_token.IsCancellationRequested)
                    break;
            }
            
            IsSpawning = false;
        }
        

        private bool IsAtLeastOneAlive()
        {
            if (_token.IsCancellationRequested)
                return false;
            foreach (var unit in _list)
            {
                if (unit.IsAlive)
                    return true;
            }

            return false;
        }
    }

    public interface IUnitFabric
    {
        IUnit CreateUnit(int type);
        void DestroyUnit(int type, IUnit obj);
    }
    
    public interface IUnit
    {
        bool IsAlive { get; }
    }

}