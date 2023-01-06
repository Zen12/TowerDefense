using System;
using _Project.Scripts.Movables;
using UnityEngine;

namespace _Project.Scripts
{
    public class EntryPoint : MonoBehaviour, IMoveControllerListener
    {
        [SerializeField] private UnityEngineBezierPathAdapter _path;

        private MovableController _controller;
        
        private void Awake()
        {
            _controller = new MovableController(_path, this);
        }

        private void Update()
        {
            _controller.Update(Time.deltaTime);
        }

        public void OnFinishMovable(IMovable movable)
        {
            
        }
    }
}