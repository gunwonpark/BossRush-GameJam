using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
// ���ҽ��� �����ϴ� Manager�Դϴ�.

public class ResourceManager
{
    // addressable�� ����ϱ� ���ϸ� �̰� �ʿ��ϴ�
    // Dictionary<string, Object> _resources = new Dictionary<string, Object>();

    public T Load<T>(string key) where T : Object
    {
        T prefab = Resources.Load<T>(key);
        return prefab;
    }

    /// <summary>
    /// �⺻������ Prefabs�� �ִ� ������ �������� �Ѵ�
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