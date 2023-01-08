using System;
using _Project.Scripts.ObjectPlacer;
using _Project.Scripts.Tower;
using UnityEngine;

namespace _Project.Scripts.Unity
{
    public sealed class TowerView : MonoBehaviour, IPlaceable, ITowerView
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Color _startColor;
        [SerializeField] private SphereCollider _trigger;

        private MaterialPropertyBlock _block;
        private static readonly int ColorId = Shader.PropertyToID("_Color");
        private static Collider[] _colliders = new Collider[30];
        private BaseTower _tower;


        public Vector3 Position
        {
            get => _tr.position;
            set => _tr.position = value;
        }

        public Bounds Bounds => _renderer.bounds;

        private Transform _tr;

        private void Awake()
        {
            _tr = transform;
            _block = new MaterialPropertyBlock();
        }

        public void SetImpossibleToPlace()
        {
            SetColor(Color.red);
        }
        
        public void SetPossibleToPlace()
        {
            SetColor(Color.green);
        }
        
        public void SetPlace()
        {
            SetColor(_startColor);
        }

        private void SetColor(Color color)
        {
            _renderer.GetPropertyBlock(_block);
            _block.SetColor(ColorId, color);
            _renderer.SetPropertyBlock(_block);
        }
        

        public void Init(BaseTower tower)
        {
            _tower = tower;
            _trigger.radius = _tower.CurrentAttackDistance;

            // if it was placed when it is running the wave, at Trigger Enter may not be executed
            var size = Physics.OverlapSphereNonAlloc(_tr.position, _tower.CurrentAttackDistance, _colliders);
            for (int i = 0; i < size; i++)
            {
                OnTriggerEnter(_colliders[i]);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_tower == null)
                return;

            var c = other.GetComponent<IDamageable>();
            if (c != null)
            {
                _tower.Add(c);
            }
        }
        

        private void OnTriggerExit(Collider other)
        {
            if (_tower == null)
                return;
            
            var c = other.GetComponent<IDamageable>();
            if (c != null)
            {
                _tower.Remove(c);
            }
        }

        private void Update()
        {
            if (_tower != null)
                _tower.Update(Time.deltaTime);
        }

        public void LookAt(Vector3 pos)
        {
            pos.y = _tr.position.y;
            _tr.LookAt(pos);
        }
    }
}
