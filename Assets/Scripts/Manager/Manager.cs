using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager _instance;
    public static Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = GameObject.Find("@Manager");
                if (go == null)
                {
                    go = new GameObject() { name = "@Manager" };
                    go.AddComponent<Manager>();
                }

                DontDestroyOnLoad(go);
                _instance = go.GetComponent<Manager>();
            }
            return _instance;
        }
    }

    ObjectManager _object = new ObjectManager();
    ResourceManager _resource = new ResourceManager();
    PoolManager _pool = new PoolManager();
    public ObjectManager Object
    {
        get
        {
            return _object;
        }
    }

    public ResourceManager Resource
    {
        get
        {
            return _resource;
        }
    }

    public PoolManager Pool
    {
        get
        {
            return _pool;
        }
    }
}

