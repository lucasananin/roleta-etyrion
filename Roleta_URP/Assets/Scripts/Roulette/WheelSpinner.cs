using UnityEngine;
using System.Collections;

public class WheelSpinner : MonoBehaviour
{
    public float spinDuration = 4f;
    public int minSpins = 4;
    public int maxSpins = 7;

    bool spinning = false;

    public void Spin()
    {
        if (!spinning)
            StartCoroutine(SpinWheel());
    }

    IEnumerator SpinWheel()
    {
        spinning = true;

        float time = 0f;
        float startAngle = transform.eulerAngles.z;

        int spins = Random.Range(minSpins, maxSpins);
        float finalAngle = startAngle + spins * 360 + Random.Range(0, 360);

        while (time < spinDuration)
        {
            time += Time.deltaTime;

            float t = time / spinDuration;
            t = Mathf.SmoothStep(0, 1, t);

            float angle = Mathf.Lerp(startAngle, finalAngle, t);
            transform.eulerAngles = new Vector3(0, 0, angle);

            yield return null;
        }

        transform.eulerAngles = new Vector3(0, 0, finalAngle);

        spinning = false;

        int result = GetResult();
        Debug.Log("Winner segment: " + result);
    }

    public int segmentCount = 8;

    int GetResult()
    {
        float angle = transform.eulerAngles.z;

        angle = 360 - angle;
        angle += 360 / segmentCount / 2;

        int result = (int)(angle / (360 / segmentCount));
        result = result % segmentCount;

        return result;
    }
}