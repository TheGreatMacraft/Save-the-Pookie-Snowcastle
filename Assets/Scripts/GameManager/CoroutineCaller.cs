using UnityEngine;

public class CoroutineCaller : MonoBehaviour
{
    public static CoroutineCaller Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}