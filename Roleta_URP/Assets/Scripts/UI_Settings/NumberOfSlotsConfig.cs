using TMPro;
using UnityEngine;

public class NumberOfSlotsConfig : ConfigBehaviour
{
    [SerializeField] TextMeshProUGUI _valueTxt = null;
    [SerializeField] int _min = 6;
    [SerializeField] int _max = 30;

    public void Increase(int _value)
    {
        _runtimeSO.NumberOfSlots += _value;
        _runtimeSO.NumberOfSlots = Mathf.Clamp(_runtimeSO.NumberOfSlots, _min, _max);
        UpdateVisuals();
    }

    public override void UpdateVisuals()
    {
        _valueTxt.text = $"{_runtimeSO.NumberOfSlots}";
    }
}
