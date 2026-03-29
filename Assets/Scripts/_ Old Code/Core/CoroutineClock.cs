using System;
using System.Collections;
using UnityEngine;

public class CoroutineClock : Clock
{
    private MonoBehaviour caller;


    public CoroutineClock(MonoBehaviour caller)
    {
        this.caller = caller;
    }
    
    
    public void Schedule(Action task, float delay)
    {
        caller.StartCoroutine(CallInTime(task, delay));
    }

    private IEnumerator CallInTime(Action task, float delay)
    {
        yield return new WaitForSeconds(delay);
        task?.Invoke();
    }
}