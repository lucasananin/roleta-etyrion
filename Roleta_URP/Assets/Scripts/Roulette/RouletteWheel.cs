using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RouletteWheel : MonoBehaviour
{
    public RectTransform wheel;
    public Button spinButton;

    public int numberOfSlots = 8;

    public float minSpinTime = 2f;
    public float maxSpinTime = 4f;

    public float minSpeed = 800f;
    public float maxSpeed = 1200f;

    bool spinning = false;

    void Start()
    {
        spinButton.onClick.AddListener(Spin);
    }

    public void Spin()
    {
        if (!spinning)
            StartCoroutine(SpinRoutine());
    }

    IEnumerator SpinRoutine()
    {
        spinning = true;
        spinButton.interactable = false;

        float duration = Random.Range(minSpinTime, maxSpinTime);
        float speed = Random.Range(minSpeed, maxSpeed);

        float time = 0;

        while (time < duration)
        {
            float t = time / duration;

            // desaceleração suave
            float currentSpeed = Mathf.Lerp(speed, 0, t);

            wheel.Rotate(0, 0, -currentSpeed * Time.deltaTime);

            time += Time.deltaTime;

            yield return null;
        }

        //wheel.Rotate(0, 0, -5f);

        DetectSlot();

        spinButton.interactable = true;
        spinning = false;
    }

    void DetectSlot()
    {
        float angle = wheel.eulerAngles.z;
        float slotAngle = 360f / numberOfSlots;
        int slot = Mathf.FloorToInt((angle % 360) / slotAngle);
        int result = numberOfSlots - slot - 1;
        //Debug.Log("Parou na casa: " + result);
        Debug.Log($"Angle={angle} Result={result}");

        //float finalAngle = (wheel.eulerAngles.z % 360) + UnityEngine.Random.Range(-10f, 10f); // Add randomness
        //int segmentIndex = -1;
        //float slotAngle = 360f / numberOfSlots;

        //for (int i = 0; i < numberOfSlots; i++)
        //{
        //    //if (finalAngle >= segmentAngles[i] && finalAngle < segmentAngles[(i + 1) % numberOfSlots])
        //    if (finalAngle >= slotAngle * i && finalAngle < slotAngle * (i + 1) % numberOfSlots)
        //    {
        //        segmentIndex = i;
        //        break;
        //    }
        //}
        ////if (segmentIndex == -1 && finalAngle >= segmentAngles[numberOfSlots - 1])
        //if (segmentIndex == -1 && finalAngle >= numberOfSlots)
        //{
        //    segmentIndex = numberOfSlots - 1;
        //}

        //Debug.Log("Parou na casa: " + segmentIndex);
    }
}