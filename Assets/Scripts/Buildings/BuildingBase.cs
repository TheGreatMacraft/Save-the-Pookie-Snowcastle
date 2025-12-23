using System;
using UnityEngine;

public abstract class BuildingBase : MonoBehaviour
{
    // Variables to be Assigned in Inspector
    public float timeToDisable;
    
    // Variables used in Script
    public bool _isEnabled;

    public bool isEnabled
    {
        get => _isEnabled;
        set
        {
            if (_isEnabled != value)
            {
                _isEnabled = value;

                if (_isEnabled)
                    OnBuildingEnable();
                else
                    OnBuildingDisable();
            }
        }
    }
    
    // Called when Building gets Enabled
    protected virtual void OnBuildingEnable()
    {
        // Register Building in Active Building Tracker List
        ActiveBuildingTracker.instance.Register(gameObject);
    }
    
    // Called when Building gets Disabled
    protected virtual void OnBuildingDisable()
    {
        // Unregister Building in Active Building Tracker List
        ActiveBuildingTracker.instance.Unregister(gameObject);
    }

    protected virtual void Start()
    {
        SetupComponents();
    }

    protected virtual void SetupComponents()
    {
        _isEnabled = false;
        isEnabled = true;
    }
}