using UnityEngine;

public abstract class Singleton<T> : Main where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null) Debug.LogError($"[Singleton] {typeof(T).Name} instance has not been created yet!");

            return _instance;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        LoadInstance();
    }

    protected virtual void LoadInstance()
    {
        if (_instance == null)
        {
            _instance = this as T;
        }
        else
        {
            //Debug.LogError($"[Singleton] Multiple instances of {typeof(T).Name} detected! Destroy the duplicate.");
        }
    }
}
