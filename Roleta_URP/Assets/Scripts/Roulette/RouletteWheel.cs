using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class RouletteWheel : MonoBehaviour
{
    [SerializeField] GameDataSO _so = null;
    [SerializeField] RectTransform _wheel = null;
    [SerializeField] Button _spinButton = null;
    [SerializeField] AnimationCurve _curve = null;

    [Header("// RUNTIME")]
    [SerializeField] int _numberOfSlots = 8;
    [SerializeField] float _minDuration = 15f;
    [SerializeField] float _maxDuration = 20f;
    [SerializeField] float _minSpeed = 4f;
    [SerializeField] float _maxSpeed = 6f;

    private bool _spinning = false;

    public static event UnityAction<RouletteWheel> OnSpinStart = null;
    public static event UnityAction<RouletteWheel> OnSpinEnd = null;

    public int NumberOfSlots { get => _numberOfSlots; }

    private void Awake()
    {
        _numberOfSlots = _so.NumberOfSlots;
        _minDuration = _so.DurationRange.x;
        _maxDuration = _so.DurationRange.y;
        _minSpeed = _so.SpeedRange.x * 360f;
        _maxSpeed = _so.SpeedRange.y * 360f;
    }

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
        float _speed = Random.Range(_minSpeed, _maxSpeed);
        Spin(_speed);
    }

    public void SpinNormalized(float _speedNormalized)
    {
        var _speed = Mathf.Lerp(_minSpeed, _maxSpeed, _speedNormalized);
        var _duration = Mathf.Lerp(_minDuration, _maxDuration, _speedNormalized);

        if (!_spinning)
            StartCoroutine(SpinRoutine(_speed, _duration));
    }

    public void Spin(float _speed)
    {
        _speed = Mathf.Clamp(_speed, _minSpeed, _maxSpeed);
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
}