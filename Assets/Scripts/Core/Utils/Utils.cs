using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    // Toggle Bool After Time
    public static void ToggleBoolInTime(
        Action<bool> setFlag,
        bool valueToToggle,
        float time,
        Delegate additionalAction = null,
        params object[] args
    )
    {
        // Toggle Bool
        var newValue = !valueToToggle;
        setFlag(newValue);

        CoroutineCaller.Instance.StartCoroutine(CallActionAfterTime(time, () =>
            {
                // Toggle Bool Back to Original Value
                setFlag(valueToToggle);

                // Run Aditional Method
                additionalAction?.DynamicInvoke(args);
            }
        ));
    }


    // Coroutine that calls Parameter Method after Given Time
    public static IEnumerator CallActionAfterTime(float time, Action action)
    {
        yield return new WaitForSeconds(time);

        action();
    }

    public static IEnumerator DoInTime(float duration, Delegate action, params object[] args)
    {
        var elasped = 0f;

        while (elasped < duration)
        {
            elasped += Time.deltaTime;

            var progress = Mathf.Clamp01(elasped / duration);

            var finalArgs = GetFinalArgs(progress, args);

            action.DynamicInvoke(finalArgs);

            yield return null;
        }
    }

    // Add Progress to Old Args 
    private static object[] GetFinalArgs(float progress, params object[] args)
    {
        var finalArgs = new object[1 + args.Length];
        finalArgs[0] = progress;
        for (var i = 0; i < args.Length; i++)
            finalArgs[i + 1] = args[i];

        return finalArgs;
    }
    
    public static GameObject[] GetObjectsInRadiousWithTag(Vector3 position, float radious, string tag)
    {
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(position, radious);

        List<GameObject> appropriateObjects = new List<GameObject>();

        foreach (var collider in collidersInRange)
        {
            if(collider.CompareTag(tag))
                appropriateObjects.Add(collider.gameObject);
        }
        
        return appropriateObjects.ToArray();
    }
}