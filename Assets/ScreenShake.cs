using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 pos = transform.position;
        float time = 0.0f;
        while(time < duration)
        {
            float xOffset = Random.Range(1.0f, 1.0f) * magnitude;
            float yOffset = Random.Range(1.0f, 1.0f) * magnitude;

            transform.position = new Vector3(pos.x + xOffset, pos.y + yOffset, pos.z);

            time += Time.deltaTime;
            yield return null;
        }
    }
}
