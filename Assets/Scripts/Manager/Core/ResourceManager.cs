using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
// 리소스를 관리하는 Manager입니다.

public class ResourceManager
{
    // addressable을 사용하기 위하면 이게 필요하다
    // Dictionary<string, Object> _resources = new Dictionary<string, Object>();

    public T Load<T>(string key) where T : Object
    {
        T prefab = Resources.Load<T>(key);
        return prefab;
    }

    /// <summary>
    /// 기본적으로 Prefabs에 있는 폴더를 기준으로 한다
    /// </summary>
    /// <param name="path"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject Instantiate(string path, Transform parent = null, bool isPooling = false)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Fail to load {path}");
            return null;
        }

        if (isPooling)
        {
            return Manager.Instance.Pool.Pop(prefab);
        }

        GameObject go = Object.Instantiate(prefab, parent);
        go.name = prefab.name;
        return go;
    }



    public void Destroy(GameObject go)
    {
        if(go == null)
        {
            return;
        }

        if (Manager.Instance.Pool.Push(go))
        {
            return;
        }

        Object.Destroy(go);
    }
}