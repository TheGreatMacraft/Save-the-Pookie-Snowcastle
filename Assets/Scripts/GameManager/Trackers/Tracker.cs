using System.Collections.Generic;
using UnityEngine;

public abstract class TrackerBase<T> : TrackerBase where T : TrackerBase<T>
{
    public static T instance { get; private set; }

    protected virtual void Awake()
    {
        if (instance == null)
            instance = this as T;
    }
}

public abstract class TrackerBase : MonoBehaviour
{
    public List<GameObject> registeredElements = new();

    public void Register(GameObject obj)
    {
        registeredElements.Add(obj);
    }

    public void Unregister(GameObject obj)
    {
        registeredElements.Remove(obj);
    }

    public bool anyElementsRegistred()
    {
        return registeredElements.Count > 0;
    }
}