using UnityEngine;
using UnityEngine.UI;

public class SlotBehaviour : MonoBehaviour
{
    [SerializeField] Image _fill = null;

    internal void Init(int _numberOfSlots)
    {
        _fill.fillAmount = 1f / _numberOfSlots * 1f;
        GetComponentInChildren<Image>().color = Random.ColorHSV(0, 1, 0.5f, 1, 0.5f, 1f);
    }
}
