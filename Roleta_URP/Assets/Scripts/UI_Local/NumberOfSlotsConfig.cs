using TMPro;
using UnityEngine;

public class NumberOfSlotsConfig : MonoBehaviour
{
    [SerializeField] GameDataSO _runtimeSO = null;
    [SerializeField] TextMeshProUGUI _valueTxt = null;
    [SerializeField] int _min = 6;
    [SerializeField] int _max = 30;

    private void Awake()
    {
        UpdateVisuals();
    }

    public void Increase(int _value)
    {
        _runtimeSO.NumberOfSlots += _value;
        _runtimeSO.NumberOfSlots = Mathf.Clamp(_runtimeSO.NumberOfSlots, _min, _max);
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        _valueTxt.text = $"{_runtimeSO.NumberOfSlots}";
    }

    public void Apply(GameDataSO _so)
    {
        _runtimeSO.NumberOfSlots = _so.NumberOfSlots;
    }
}
