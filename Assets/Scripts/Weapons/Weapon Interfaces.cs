using UnityEngine;

public interface Payload
{
    void Deliver(GameObject potentialTarget);
    void Deliver(ReadOnlyCollection<GameObject> potentialTargets);
}

public interface WeaponActions
{
    public ActionExecution DefaultAttack();
    public ActionExecution HeavyAttack();
    public ActionExecution SupportAction();
    public ActionExecution Ability();
}

public interface Weapon : WeaponActions {}