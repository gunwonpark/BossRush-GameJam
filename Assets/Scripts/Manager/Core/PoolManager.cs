using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Pool
{
    GameObject _prefab;
    IObjectPool<GameObject> _pool;

    Transform _root;

    Transform Root
    {
        get
        {
            if(_root == null)
            {
                _root = new GameObject() { name = $"{_prefab.name} Root" }.transform;
            }

            return _root;
        }
    }

    public Pool(GameObject prefab)
    {
        _prefab = prefab;
        _pool = new ObjectPool<GameObject>(Create, Get, Release, Destroy);
    }
    public GameObject Pop()
    {
        return _pool.Get();
    }
    public void Push(GameObject go)
    {
        _pool.Release(go);
    }
    private void Destroy(GameObject go)
    {
        GameObject.Destroy(go);
    }

    private void Release(GameObject go)
    {
        go.SetActive(false);
    }

    private void Get(GameObject go)
    {
        go.SetActive(true);
    }

    private GameObject Create()
    {
        GameObject go = GameObject.Instantiate(_prefab);
        go.transform.parent = Root;
        go.name = _prefab.name;
        return go;
    }

}
public class PoolManager
{
    private Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();

    private void CreatePool(GameObject prefab)
    {
        if (_pool.ContainsKey(prefab.name))
        {
            return;
        }
        _pool.Add(prefab.name, new Pool(prefab));
    }

    public GameObject Pop(GameObject go)
    {
        if (_pool.ContainsKey(go.name) == false)
        {
            CreatePool(go);
        }

        return _pool[go.name].Pop();
    }

    public bool Push(GameObject go)
    {
        if (_pool.ContainsKey(go.name) == false)
        {
            Debug.LogError($"PoolManager.Push() : {go.name} Not Exist Pool");
            return false;
        }

        _pool[go.name].Push(go);
        return true;
    }
}
