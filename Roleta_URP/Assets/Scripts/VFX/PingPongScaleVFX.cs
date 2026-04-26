using UnityEngine;

public class PingPongScaleVFX : MonoBehaviour
{
    [SerializeField] Vector3 _minScale = Vector3.one * 0.5f;
    [SerializeField] Vector3 _maxScale = Vector3.one;
    //[SerializeField] AnimationCurve _curve = null;
    [SerializeField] float _speed = 1f;

    private float _timer = 0f;
    private float _t = 0f;
    //private float _c = 0f;

    private void Awake()
    {
        _timer = Random.Range(0f, 1f);
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        _t = Mathf.PingPong(_timer * _speed, 1f);
        //_c = _curve.Evaluate(_t);
        transform.localScale = Vector3.Lerp(_minScale, _maxScale, _t);
    }
}
