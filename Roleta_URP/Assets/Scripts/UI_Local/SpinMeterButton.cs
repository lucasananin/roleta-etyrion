using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpinMeterButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] Image _fill = null;
    [SerializeField] Gradient _color = null;

    private float _timer = 0f;
    private bool _isHolding = false;

    private void Awake()
    {
        ResetValues();
    }

    private void Update()
    {
        if (_isHolding)
        {
            _fill.fillAmount = Mathf.PingPong(_timer, 1);
            _fill.color = _color.Evaluate(_fill.fillAmount);
            _timer += Time.deltaTime;
        }
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
        _wheel.Spin(_fill.fillAmount * 4000 + 1000, 0f);

        ResetValues();
    }

    private void ResetValues()
    {
        _timer = 0;
        _fill.fillAmount = 0;
    }
}
