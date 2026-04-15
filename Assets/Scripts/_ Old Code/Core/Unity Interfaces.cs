using System;

public interface Clock
{
    void Schedule(Action task, float delay);
    void DoUntil(Action<float> task, float duration, Action onFinished = null);
}