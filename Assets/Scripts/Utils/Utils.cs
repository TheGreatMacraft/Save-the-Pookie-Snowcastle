using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
        bool newValue = !valueToToggle;
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

    public static IEnumerator DoInTime(float duration, Delegate action,  params object[] args)
    {
        float elasped = 0f;

        while(elasped < duration)
        {
            elasped += Time.deltaTime;

            float progress = Mathf.Clamp01(elasped / duration);

            object[] finalArgs = GetFinalArgs(progress, args);

            action.DynamicInvoke(finalArgs);

            yield return null;
        }
    }

    // Add Progress to Old Args 
    private static object[] GetFinalArgs(float progress, params object[] args)
    {
        object[] finalArgs = new object[1 + args.Length];
        finalArgs[0] = progress;
        for (int i = 0; i < args.Length; i++)
            finalArgs[i + 1] = args[i];

        return finalArgs;
    }
}
