using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public ProjectileType type;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    private Dictionary<ProjectileType, Queue<GameObject>> _poolDictionary;
    [SerializeField] private bool _expandable = true;

    private void Awake()
    {
        _poolDictionary = new Dictionary<ProjectileType, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>(pool.size);

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            _poolDictionary[pool.type] = objectPool;
        }
    }

    public GameObject SpawnFromPool(ProjectileType type, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.TryGetValue(type, out Queue<GameObject> q))
        {
            return null;
        }

        int count = q.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject candidate = q.Dequeue();
            q.Enqueue(candidate);

            if (candidate.activeSelf == false)
            {
                candidate.transform.SetPositionAndRotation(position, rotation);
                candidate.SetActive(true);
                return candidate;
            }
        }

        if (_expandable)
        {
            GameObject prefab = null;

            for (int i = 0; i < pools.Count; i++)
            {
                if (pools[i].type == type)
                {
                    prefab = pools[i].prefab;
                    break;
                }
            }

            if (prefab != null)
            {
                GameObject extra = Instantiate(prefab);
                extra.transform.SetPositionAndRotation(position, rotation);
                extra.SetActive(true);

                q.Enqueue(extra);

                return extra;
            }
            else
            {
                return null;
            }
        }

        return null;
    }
}
