using UnityEngine;
using System.Collections;

public class CupWobbleAI : MonoBehaviour
{
    public float wobbleAngle = 5f;
    public float wobbleIntervalMin = 3f;
    public float wobbleIntervalMax = 6f;

    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.rotation;
        StartCoroutine(WobbleLoop());
    }

    IEnumerator WobbleLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(wobbleIntervalMin, wobbleIntervalMax));

            float angle = Random.Range(-wobbleAngle, wobbleAngle);
            Quaternion tilt = Quaternion.Euler(angle, 0, 0);
            transform.rotation = originalRotation * tilt;

            yield return new WaitForSeconds(0.5f);
            transform.rotation = originalRotation;
        }
    }
}
