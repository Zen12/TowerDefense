using _Project.Scripts.Database;
using _Project.Scripts.ObjectPlacer;
using _Project.Scripts.Unity.UI;
using UnityEngine;

namespace _Project.Scripts.Unity
{
    public sealed class TowerPlacerUnityController : ISelectTowerItem, IUpdatable
    {
        private readonly Camera _camera;
        private readonly ObjectBoundsPlacer _placer;
        private bool _isSelected;
        private readonly LayerMask _mask;
        private TowerView _view;

        public TowerPlacerUnityController(ObjectBoundsPlacer boundsPlacer, Camera camera, LayerMask mask)
        {
            _mask = mask;
            _placer = boundsPlacer;
            _camera = camera;
        }
        
        public void OnSelectTower(TowerItem item)
        {
            _isSelected = true;
            _view = GameObject.Instantiate(item.Tower);
        }

        public void Update(in float deltaTime)
        {
            if (_isSelected == false)
                return;

            if (Input.GetMouseButtonUp(0))
            {
                //place object
                _isSelected = false;
                if (_placer.Place(_view))
                {
                    _view.SetPlace();
                }
                else
                {
                    GameObject.Destroy(_view.gameObject);
                    _view = null;
                }
                return;
            }

            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var raycastHi, float.MaxValue, _mask))
                {
                    _view.transform.position = raycastHi.point;
                    if (_placer.IsAbleToPlace(raycastHi.point))
                    {
                        _view.SetPossibleToPlace();
                    }
                    else
                    {
                        _view.SetImpossibleToPlace();
                    }
                }
            }
        }
        
    }
}
