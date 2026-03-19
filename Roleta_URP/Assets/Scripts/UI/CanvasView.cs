using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class CanvasView : MonoBehaviour
{
    [SerializeField] CanvasGroup _canvasGroup = null;
    [SerializeField] float _fadeDuration = 0.3f;
    [SerializeField] bool _hideOnAwake = true;
    [Space]
    [SerializeField] UnityEvent _onShow = null;
    [SerializeField] UnityEvent _onHide = null;

    private float _lastAlpha = 0;
    private float _targetAlpha = 0;
    private float _timer = 0;

    private void Awake()
    {
        if (_hideOnAwake)
        {
            InstantHide();
        }
        else
        {
            InstantShow();
        }
    }

    private void OnValidate()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    //protected virtual void Update()
    //{
    //    if (_canvasGroup.alpha == _targetAlpha) return;

    //    _timer += Time.unscaledDeltaTime * (1f / _fadeDuration);
    //    _canvasGroup.alpha = Mathf.Lerp(_lastAlpha, _targetAlpha, _timer);
    //}

    public virtual void Show()
    {
        //_timer = 0;
        //_targetAlpha = 1;
        //_lastAlpha = _canvasGroup.alpha;
        //_canvasGroup.blocksRaycasts = true;
        Fade(1, _canvasGroup.alpha, _canvasGroup.alpha, true);
        _onShow?.Invoke();
    }

    public virtual void Hide()
    {
        //_timer = 0;
        //_targetAlpha = 0;
        //_lastAlpha = _canvasGroup.alpha;
        //_canvasGroup.blocksRaycasts = false;
        Fade(0, _canvasGroup.alpha, _canvasGroup.alpha, false);
        _onHide?.Invoke();
    }

    public virtual void InstantShow()
    {
        //_timer = 1;
        //_targetAlpha = 1;
        //_lastAlpha = 1;
        //_canvasGroup.alpha = 1;
        //_canvasGroup.blocksRaycasts = true;
        Fade(1, 1, 1, true);
        _onShow?.Invoke();
    }

    public virtual void InstantHide()
    {
        //_timer = 0;
        //_targetAlpha = 0;
        //_lastAlpha = 0;
        //_canvasGroup.alpha = 0;
        //_canvasGroup.blocksRaycasts = false;
        Fade(0, 0, 0, false);
        _onHide?.Invoke();
    }

    private void Fade(float _target, float _previous, float _alpha, bool _blockRaycasts)
    {
        _timer = 0;
        _targetAlpha = _target;
        _lastAlpha = _previous;
        _canvasGroup.alpha = _alpha;
        _canvasGroup.blocksRaycasts = _blockRaycasts;
        StopAllCoroutines();
        StartCoroutine(Fade_Routine());
    }

    private IEnumerator Fade_Routine()
    {
        while (_canvasGroup.alpha != _targetAlpha)
        {
            _timer += Time.unscaledDeltaTime * (1f / _fadeDuration);
            _canvasGroup.alpha = Mathf.Lerp(_lastAlpha, _targetAlpha, _timer);
            yield return null;
        }
    }

    public bool IsVisible()
    {
        return _canvasGroup.alpha > 0;
    }
}
