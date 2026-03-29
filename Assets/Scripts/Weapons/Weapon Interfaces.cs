using UnityEngine;

public interface Payload
{
    void Deliver(GameObject potentialTarget);
    void Deliver(ReadOnlyCollection<GameObject> potentialTargets);
}