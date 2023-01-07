using System;
using _Project.Scripts.ObjectPlacer;
using UnityEngine;

namespace _Project.Scripts.Unity
{
    public sealed class TowerView : MonoBehaviour, IPlaceable
    {
        [SerializeField] private Renderer _renderer;
        private MaterialPropertyBlock _block;
        [SerializeField] private Color _startColor;
        private static readonly int ColorId = Shader.PropertyToID("_Color");


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

    }
}
