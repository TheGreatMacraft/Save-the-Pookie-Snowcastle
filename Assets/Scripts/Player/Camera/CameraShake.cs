using System.Collections;
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
        var originalPos = transform.localPosition;
        float timeElapsed = 0;

        while (timeElapsed < durationInSeconds)
        {
            var x = Random.Range(-1f, 1f) * magnitude;
            var y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}