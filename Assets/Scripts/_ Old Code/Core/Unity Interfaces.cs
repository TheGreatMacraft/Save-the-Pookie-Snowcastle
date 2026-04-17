using System;

public interface Clock
{
    void Schedule(Action task, float delay);
    void Schedule(Action task, float delay, Condition extraCondition, Action onCanceled = null);
    void DoUntil(Action<float> task, float duration, Action onFinished = null);
}