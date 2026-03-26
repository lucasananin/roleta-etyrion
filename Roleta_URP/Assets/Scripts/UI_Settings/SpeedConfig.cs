using TMPro;
using UnityEngine;

public class SpeedConfig : ConfigBehaviour
{
    [SerializeField] TextMeshProUGUI _minTxt = null;
    [SerializeField] TextMeshProUGUI _maxTxt = null;
    [SerializeField] int _minSpeed = 4;
    [SerializeField] int _maxSpeed = 6;

    public void IncreaseMin(int _value)
    {
        var _newValue = _runtimeSO.SpeedRange.x + _value;
        var _clampedValue = Mathf.Clamp(_newValue, _minSpeed, _maxSpeed);
        _runtimeSO.SpeedRange = new(_clampedValue, _runtimeSO.SpeedRange.y);
        UpdateVisuals();
    }

    public void IncreaseMax(int _value)
    {
        var _newValue = _runtimeSO.SpeedRange.y + _value;
        var _clampedValue = Mathf.Clamp(_newValue, _minSpeed, _maxSpeed);
        _runtimeSO.SpeedRange = new(_runtimeSO.SpeedRange.x, _clampedValue);
        UpdateVisuals();
    }

    public override void UpdateVisuals()
    {
        _minTxt.text = $"{_runtimeSO.SpeedRange.x}";
        _maxTxt.text = $"{_runtimeSO.SpeedRange.y}";
    }
}
