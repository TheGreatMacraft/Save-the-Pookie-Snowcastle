public interface AnimatedRun
{
    void ToggleWalking(bool value);
}

public interface AnimatedRoll
{
    void TriggerRolling();
}

public interface PlayerAnimationMessenger 
    :  AnimatedRun, AnimatedRoll {}
