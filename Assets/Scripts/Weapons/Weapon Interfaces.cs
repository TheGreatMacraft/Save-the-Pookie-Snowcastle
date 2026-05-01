using UnityEngine;

public interface Payload
{
    void Deliver(GameObject potentialTarget);
    void Deliver(ReadOnlyCollection<GameObject> potentialTargets);
}

public interface Weapon
{
    public ActionExecution DefaultAttack();
    public ActionExecution HeavyAttack();
    public ActionExecution SupportAction();
    public ActionExecution Ability();
}