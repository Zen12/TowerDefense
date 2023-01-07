using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Movables
{
    public sealed class MovableController : IUpdatable
    {
        private readonly IPath _path;
        private readonly List<MovableData> _list = new List<MovableData>();
        private readonly IMoveControllerListener _listener;

        public MovableController(IPath path, IMoveControllerListener listener)
        {
            _path = path;
            _listener = listener;
        }

        public void RegisterMovable(IMovable movable, float speed)
        {
            movable.Position = _path.GetPositionFromTime(0);
            _list.Add(new MovableData
            {
                Speed = speed,
                Target = movable,
                NeedToRemove = false
            });
        }

        public void Update(in float deltaTime)
        {
            foreach (var data in _list)
            {
                var time = _path.GetTime(data.Target.Position);
                if (time >= 1 - deltaTime - float.Epsilon)
                {
                    data.Target.Position = _path.GetPositionFromTime(1f);
                    data.NeedToRemove = true;
                    _listener.OnFinishMovable(data.Target);
                    continue;
                }

                var nexTime = time + deltaTime;
                var nextPos = _path.GetPositionFromTime(nexTime);
                var dir = (nextPos - data.Target.Position).normalized;
                data.Target.Position += dir * data.Speed;
                data.Target.Rotation = Quaternion.LookRotation(dir);
            }

            _list.RemoveAll(_ => _.NeedToRemove);
        }
    }
    
    
    internal sealed class MovableData
    {
        public IMovable Target;
        public float Speed;
        public bool NeedToRemove;
    }

    public interface IPath
    {
        public Vector3 GetPositionFromTime(in float time);
        public float GetTime(Vector3 position);
    }

    public interface IMoveControllerListener
    {
        void OnFinishMovable(IMovable movable);
    }

    public interface IMovable
    {
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
    }
}