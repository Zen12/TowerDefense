using System.Threading;
using _Project.Scripts.Movables;
using _Project.Scripts.SpawnSystems;

namespace _Project.Scripts.Gameplay
{
    public sealed class GameplayController : IMoveControllerListener, IWaveListener
    {
        private readonly CancellationToken _token;
        
        private MovableController _movable;
        private WaveController _wave;


        public GameplayController(CancellationToken token)
        {
            _token = token;
        }

        public void Init(MovableController movable, WaveController wave)
        {
            _movable = movable;
            _wave = wave;
        }

        public void Update(in float deltaTime)
        {
            if (_token.IsCancellationRequested)
                return;
            
            _movable.Update(deltaTime);
            _wave.Update();
        }

        public void OnFinishMovable(IMovable movable)
        {
            // Here is win system
        }

        public void OnCreateUnit(IUnit unit)
        {
            var move = (IMovable)unit;
            if (move != null)
            {
                _movable.RegisterMovable(move, 0.1f);
            }
        }
    }
}