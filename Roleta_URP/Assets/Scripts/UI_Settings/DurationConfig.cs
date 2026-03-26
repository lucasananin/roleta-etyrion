using TMPro;
using UnityEngine;

public class DurationConfig : ConfigBehaviour
{
    [SerializeField] TextMeshProUGUI _minTxt = null;
    [SerializeField] TextMeshProUGUI _maxTxt = null;
    [SerializeField] int _minDuration = 10;
    [SerializeField] int _maxDuration = 30;

    public void IncreaseMin(int _value)
    {
        var _newValue = _runtimeSO.DurationRange.x + _value;
        var _clampedValue = Mathf.Clamp(_newValue, _minDuration, _maxDuration);
        _runtimeSO.DurationRange = new(_clampedValue, _runtimeSO.DurationRange.y);
        UpdateVisuals();
    }

    public void IncreaseMax(int _value)
    {
        var _newValue = _runtimeSO.DurationRange.y + _value;
        var _clampedValue = Mathf.Clamp(_newValue, _minDuration, _maxDuration);
        _runtimeSO.DurationRange = new(_runtimeSO.DurationRange.x, _clampedValue);
        UpdateVisuals();
    }

    public override void UpdateVisuals()
    {
        _minTxt.text = $"{_runtimeSO.DurationRange.x}";
        _maxTxt.text = $"{_runtimeSO.DurationRange.y}";
    }
}
