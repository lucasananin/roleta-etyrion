using TMPro;
using UnityEngine;

public class NumberPickedView : CanvasView
{
    [SerializeField] TextMeshProUGUI _text = null;

    private void OnEnable()
    {
        RouletteWheel.OnSpinEnd += UpdateVisuals;
    }

    private void OnDisable()
    {
        RouletteWheel.OnSpinEnd -= UpdateVisuals;
    }

    private void UpdateVisuals(RouletteWheel _wheel)
    {
        Show();
        _text.text = $"{_wheel.Result}";
    }
}
