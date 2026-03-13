using UnityEngine;
using UnityEngine.UI;

public class SlotBehaviour : MonoBehaviour
{
    internal void RandomizeColor()
    {
        GetComponentInChildren<Image>().color = Random.ColorHSV(0, 1, 0.5f, 1, 0.5f, 1f);
    }
}
