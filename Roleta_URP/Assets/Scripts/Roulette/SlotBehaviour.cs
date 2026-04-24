using UnityEngine;
using UnityEngine.UI;

public class SlotBehaviour : MonoBehaviour
{
    [SerializeField] Image _fill = null;

    internal void Init(int _numberOfSlots, Color _color)
    {
        _fill.fillAmount = 1f / _numberOfSlots * 1f;
        _fill.color = _color;
    }
}
