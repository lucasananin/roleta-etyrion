using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class RouletteWheel : MonoBehaviour
{
    [SerializeField] RectTransform _wheel = null;
    [SerializeField] Button _spinButton = null;
    [SerializeField] AnimationCurve _curve = null;
    [SerializeField] int _numberOfSlots = 8;
    [Space]
    [SerializeField] float _minDuration = 15f;
    [SerializeField] float _maxDuration = 20f;
    [Space]
    [SerializeField] float _minSpeed = 4f;
    [SerializeField] float _maxSpeed = 6f;

    private bool _spinning = false;

    public static event UnityAction<RouletteWheel> OnSpinStart = null;
    public static event UnityAction<RouletteWheel> OnSpinEnd = null;

    public int NumberOfSlots { get => _numberOfSlots; }

    private IEnumerator Start()
    {
        yield return null;
        var _initialOffset = -GetSlotAngle() / 2f;
        _wheel.Rotate(0, 0, _initialOffset);
    }

    private void OnEnable()
    {
        _spinButton.onClick.AddListener(SpinRandomly);
    }

    private void OnDisable()
    {
        _spinButton.onClick.RemoveListener(SpinRandomly);
    }

    public void SpinRandomly()
    {
        //float _speed = Random.Range(GetSpin(_minSpeed), GetSpin(_maxSpeed));
        float _speed = Random.Range(_minSpeed, _maxSpeed);
        //float _duration = Random.Range(_minSpinTime, _maxSpinTime);
        Spin(_speed/*, _duration*/);
    }

    public void SpinNormalized(float _speedNormalized)
    {
        var _speed = Mathf.Lerp(_minSpeed, _maxSpeed, _speedNormalized);
        var _duration = Mathf.Lerp(_minDuration, _maxDuration, _speedNormalized);

        if (!_spinning)
            StartCoroutine(SpinRoutine(_speed, _duration));
    }

    public void Spin(float _speed/*, float _duration*/)
    {
        _speed = Mathf.Clamp(_speed, _minSpeed, _maxSpeed);
        //_duration = _minSpinTime + _speed / 1000f;
        var _speedNormalized = Mathf.InverseLerp(_minSpeed, _maxSpeed, _speed);
        var _duration = Mathf.Lerp(_minDuration, _maxDuration, _speedNormalized);

        if (!_spinning)
            StartCoroutine(SpinRoutine(_speed, _duration));
    }

    private IEnumerator SpinRoutine(float _speed, float _duration)
    {
        Debug.Log($"Speed = {_speed} / Duration = {_duration}");
        _spinning = true;
        _spinButton.interactable = false;

        //float _duration = Random.Range(_minSpinTime, _maxSpinTime);
        //float _speed = Random.Range(GetSpin(_minSpeed), GetSpin(_maxSpeed));

        float _time = 0;
        OnSpinStart?.Invoke(this);

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

        OnSpinEnd?.Invoke(this);
    }

    private void DetectSlot()
    {
        float _angle = _wheel.eulerAngles.z;
        float _slotAngle = GetSlotAngle();
        int _slot = Mathf.FloorToInt((_angle % 360) / _slotAngle);
        int _result = _numberOfSlots - _slot - 1;
        Debug.Log($"Result={_result}");
    }

    public float GetSlotAngle()
    {
        return 360f / _numberOfSlots;
    }

    //private float GetSpin(float _value)
    //{
    //    return _value * 360;
    //}
}