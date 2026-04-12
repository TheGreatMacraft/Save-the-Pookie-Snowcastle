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

    public void DoUntil(Action<float> task, float duration, Action onFinished = null)
    {
        caller.StartCoroutine(RunProcess(task, duration, onFinished));
    }
    

    private IEnumerator CallInTime(Action task, float delay)
    {
        yield return new WaitForSeconds(delay);
        task?.Invoke();
    }
    
    private IEnumerator RunProcess(Action<float> task, float duration, Action onFinished)
    {
        float timeElapsed = 0;
        
        while (timeElapsed < duration)
        {
            yield return new WaitForFixedUpdate(); 
            
            timeElapsed += Time.deltaTime;
            task?.Invoke(timeElapsed / duration);
        }
        
        onFinished?.Invoke();
    }
}