using System;

public interface Clock
{
    void Schedule(Action task, float delay);
}