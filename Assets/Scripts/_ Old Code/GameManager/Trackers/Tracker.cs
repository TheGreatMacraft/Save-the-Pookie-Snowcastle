using System;
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
    
    public event Action OnAllUnregistered;

    public void Register(GameObject obj)
    {
        if(!registeredElements.Contains(obj))
            registeredElements.Add(obj);
    }

    public void Unregister(GameObject obj)
    {
        if(registeredElements.Contains(obj))
            registeredElements.Remove(obj);

        if (!anyElementsRegistred())
            OnAllUnregistered?.Invoke();
    }

    public bool anyElementsRegistred()
    {
        return registeredElements.Count > 0;
    }
}