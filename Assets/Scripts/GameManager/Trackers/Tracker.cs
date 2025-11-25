using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class TrackerBase<T> : MonoBehaviour where T : TrackerBase<T>
{
    public static T instance { get; private set; }

    public List<GameObject> registeredElements = new List<GameObject>();

    public void Register(GameObject obj)
    {
        registeredElements.Add(obj);
    }

    public void Unregister(GameObject obj)
    {
        registeredElements.Remove(obj);
    }

    public bool anyElementsRegistred() { return registeredElements.Count > 0; }

    protected virtual void Awake()
    {
        if (instance == null)
            instance = this as T;
    }
}
