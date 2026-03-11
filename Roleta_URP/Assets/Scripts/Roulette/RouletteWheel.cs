using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RouletteWheel : MonoBehaviour
{
    [SerializeField] RectTransform _wheel = null;
    [SerializeField] Button _spinButton = null;
    [SerializeField] AnimationCurve _curve = null;
    [SerializeField] int _numberOfSlots = 8;
    [Space]
    [SerializeField] float _minSpinTime = 10f;
    [SerializeField] float _maxSpinTime = 15f;
    [Space]
    [SerializeField] float _minSpeed = 4f;
    [SerializeField] float _maxSpeed = 6f;

    private bool _spinning = false;

    private void Awake()
    {
        var _initialOffset = (360f / _numberOfSlots) / 2f;
        _wheel.Rotate(0, 0, -_initialOffset);
    }

    private void OnEnable()
    {
        _spinButton.onClick.AddListener(Spin);
    }

    private void OnDisable()
    {
        _spinButton.onClick.RemoveListener(Spin);
    }

    public void Spin()
    {
        Spin(Random.Range(GetSpin(_minSpeed), GetSpin(_maxSpeed)));
    }

    public void Spin(float _speed)
    {
        if (!_spinning)
            StartCoroutine(SpinRoutine(_speed));
    }

    private IEnumerator SpinRoutine(float _speed)
    {
        _spinning = true;
        _spinButton.interactable = false;

        float _duration = Random.Range(_minSpinTime, _maxSpinTime);
        //float _speed = Random.Range(GetSpin(_minSpeed), GetSpin(_maxSpeed));

        float _time = 0;

        while (_time < _duration)
        {
            float _t = _time / _duration;
            var _c = _curve.Evaluate(_t);
            float _currentSpeed = Mathf.Lerp(_speed, 0, _c);
            _wheel.Rotate(0, 0, -_currentSpeed * Time.deltaTime);
            _time += Time.deltaTime;
            yield return null;
        }

        DetectSlot();
        _spinButton.interactable = true;
        _spinning = false;
    }

    private void DetectSlot()
    {
        float _angle = _wheel.eulerAngles.z;
        float _slotAngle = 360f / _numberOfSlots;
        int _slot = Mathf.FloorToInt((_angle % 360) / _slotAngle);
        int _result = _numberOfSlots - _slot - 1;
        Debug.Log($"Angle={_angle} Result={_result}");
    }

    private float GetSpin(float _value)
    {
        return _value * 360;
    }
}