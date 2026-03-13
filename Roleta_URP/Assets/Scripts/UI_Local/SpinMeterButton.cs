using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpinMeterButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image _fill = null;
    [SerializeField] Gradient _color = null;
    [SerializeField] CanvasView _mainView = null;
    [SerializeField] CanvasView _fillView = null;
    [SerializeField] float _speed = 1f;

    private float _timer = 0f;
    private bool _isHolding = false;

    private void Awake()
    {
        ResetValues();
    }

    private void OnEnable()
    {
        RouletteWheel.OnSpinStart += Hide;
        RouletteWheel.OnSpinEnd += Show;
    }

    private void OnDisable()
    {
        RouletteWheel.OnSpinStart -= Hide;
        RouletteWheel.OnSpinEnd -= Show;
    }

    private void Update()
    {
        if (_isHolding)
        {
            _fill.fillAmount = Mathf.PingPong(_timer * _speed, 1);
            _fill.color = _color.Evaluate(_fill.fillAmount);
            _timer += Time.deltaTime;
        }

        if (_isHolding)
            _fillView.InstantShow();
        else
            _fillView.InstantHide();
    }

    private void Show(RouletteWheel arg0)
    {
        _mainView.InstantShow();
    }

    private void Hide(RouletteWheel arg0)
    {
        _mainView.InstantHide();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isHolding = true;
        ResetValues();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHolding = false;

        var _wheel = FindFirstObjectByType<RouletteWheel>();
        _wheel.SpinNormalized(_fill.fillAmount);
        //_wheel.Spin(_fill.fillAmount * 4000 + 1000);

        ResetValues();
    }

    private void ResetValues()
    {
        _timer = 0;
        _fill.fillAmount = 0;
    }
}
