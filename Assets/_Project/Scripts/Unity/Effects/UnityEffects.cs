using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Unity.Effects
{
    public class UnityEffects : MonoBehaviour
    {
        private float _trajectorySpeed = 10f;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private ParticleSystem _aoeAttack;
        [SerializeField] private ParticleSystem _aoeSlow;
        private readonly List<Transform> _list = new List<Transform>();

        public void ProjectileAttack(Vector3 startPos, Transform target, System.Action onFinish)
        {
            var o = GetObject();
            StartCoroutine(ProjectileRoutineToTransform(startPos, target, o.transform, () =>
            {
                SetObject(o);
                onFinish?.Invoke();
            }));
        }
        
        public void ProjectileAoeAttack(Vector3 startPos, Vector3 finish, System.Action onFinish)
        {
            var o = GetObject();
            StartCoroutine(ProjectileRoutine(startPos, finish, o.transform, () =>
            {
                _aoeAttack.transform.position = finish;
                _aoeAttack.Emit(7);
                SetObject(o);
                onFinish?.Invoke();
            }));
        }
        
        public void ProjectileSlow(Vector3 startPos, Transform target, System.Action onFinish)
        {
            var o = GetObject();
            StartCoroutine(ProjectileRoutineToTransform(startPos, target, o.transform, () =>
            {
                SetObject(o);
                onFinish?.Invoke();
            }));

        }

        public void ProjectileAoeSlow(Vector3 startPos, Vector3 finish, System.Action onFinish)
        {
            var o = GetObject();
            StartCoroutine(ProjectileRoutine(startPos, finish, o.transform, () =>
            {
                _aoeSlow.transform.position = finish;
                _aoeSlow.Emit(7);
                SetObject(o);
                onFinish?.Invoke();
            }));
        }

        private Transform GetObject()
        {
            Transform o = null;
            if (_list.Count == 0)
            {
                o = Instantiate(_prefab).transform;
            }
            else
            {
                o = _list[0].transform;
                _list.RemoveAt(0);
                o.gameObject.SetActive(true);
            }

            return o;
        }

        private void SetObject(Transform o)
        {
            o.gameObject.SetActive(false);
            _list.Add(o);
        }


        private IEnumerator ProjectileRoutine(Vector3 start, Vector3 end,
            Transform tr, System.Action finish)
        {
            var lerpTime = 0f;
            while (lerpTime < 1f)
            {
                lerpTime += Time.deltaTime * _trajectorySpeed;
                tr.position = Vector3.Lerp(start, end, lerpTime);
                yield return null;
            }
            
            finish?.Invoke();
        }
        
        private IEnumerator ProjectileRoutineToTransform(Vector3 start, Transform end,
            Transform tr, System.Action finish)
        {
            var lerpTime = 0f;
            while (lerpTime < 1f)
            {
                lerpTime += Time.deltaTime * _trajectorySpeed;
                tr.position = Vector3.Lerp(start, end.position, lerpTime);
                yield return null;
            }
            
            finish?.Invoke();
        }
    }
}
