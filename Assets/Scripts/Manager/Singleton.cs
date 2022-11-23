using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    private bool isInit;

    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)GameObject.FindObjectOfType(typeof(T));
                if (_instance == null)
                {
                    GameObject container = new GameObject();
                    container.name = typeof(T).Name;
                    _instance = container.AddComponent(typeof(T)) as T;
                    DontDestroyOnLoad(container);
                }
            }
            return _instance;
        }
    }

    public void Init()
    {
        if (isInit) return;

        isInit = true;
        InitData();
        PLogger.Log(this.name + "initialized");
    }

    public abstract void InitData();
    public abstract void ClearData();
}
