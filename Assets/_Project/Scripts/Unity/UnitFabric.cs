using System.Collections.Generic;
using _Project.Scripts.Database;
using _Project.Scripts.SpawnSystems;
using UnityEngine;

namespace _Project.Scripts.Unity
{
    public sealed class UnitFabric : IUnitFabric
    {
        private UnitConfig _config;

        private readonly Dictionary<int, List<UnitView>> _cache 
            = new Dictionary<int, List<UnitView>>();

        public UnitFabric(UnitConfig config)
        {
            _config = config;
        }

        public IUnit CreateUnit(int type)
        {
            // Take from pool
            if (_cache.ContainsKey(type) == true)
            {
                var list = _cache[type];
                if (list.Count > 0)
                {
                    var o = list[0];
                    o.gameObject.SetActive(true);
                    list.RemoveAt(0);
                    return o;
                }
            }
            
            // Create NewONe
            var p = _config.Prefabs[type];
            var obj = GameObject.Instantiate(p);
            return obj;
        }

        public void DestroyUnit(int type, IUnit obj)
        {
            if (_cache.ContainsKey(type) == false)
            {
                _cache.Add(type, new List<UnitView>());
            }
            var list = _cache[type];
            var o = (UnitView)obj;
            if (o != null)
            {
                o.gameObject.SetActive(false);
                list.Add(o);
            }
        }
    }
}