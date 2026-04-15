using System;
using UnityEngine;

public interface Payload
{
    void Deliver(GameObject potentialTarget);
    void Deliver(ReadOnlyCollection<GameObject> potentialTargets);
}

public interface WeaponActions
{
    public ActionExecution PrimaryAction();
    public ActionExecution SecondaryAction();
    public ActionExecution SupportAction();
    public ActionExecution AbilityAction();
}