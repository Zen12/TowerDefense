using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Movables
{
    public sealed class MovableController
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
            _list.Add(new MovableData
            {
                Speed = speed,
                Target = movable,
                Time = 0f
            });
        }

        public void Update(in float deltaTime)
        {
            foreach (var data in _list)
            {
                data.Time += deltaTime * data.Speed;
                if (data.Time >= 1f)
                {
                    data.Target.Position = _path.GetPositionFromTime(1f);
                    _listener.OnFinishMovable(data.Target);
                    continue;
                }
                var target = _path.GetPositionFromTime(data.Time);
                var dir = (target - data.Target.Position).normalized;
                data.Target.Position += target;
                data.Target.Rotation = Quaternion.LookRotation(dir);
            }

            _list.RemoveAll(_ => _.Time >= 1f);
        }
    }
    
    
    internal sealed class MovableData
    {
        public IMovable Target;
        public float Time;
        public float Speed;
    }

    public interface IPath
    {
        public Vector3 GetPositionFromTime(in float time);
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