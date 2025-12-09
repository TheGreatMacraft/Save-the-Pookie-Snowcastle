using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    public IEnumerator ShakeCamera(float magnitude, float durationInSeconds)
    {
        Vector3 originalPos = transform.localPosition;
        float timeElapsed = 0;

        while (timeElapsed < durationInSeconds)
        {
            float x = Random.Range(-1f,1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
