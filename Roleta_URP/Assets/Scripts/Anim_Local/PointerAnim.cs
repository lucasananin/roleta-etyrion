using UnityEngine;

public class PointerAnim : MonoBehaviour
{
    [SerializeField] RouletteWheel _wheel = null;
    [SerializeField] int _playRate = 2;
    [SerializeField] float _zRotation = 15f;

    private int _nextPlay = 0;
    private float _timer = 0;

    private void Awake()
    {
        _wheel = FindFirstObjectByType<RouletteWheel>();
    }

    private void OnEnable()
    {
        _wheel.OnSlotChanged += TryPlay;
    }

    private void OnDisable()
    {
        _wheel.OnSlotChanged -= TryPlay;
    }

    private void LateUpdate()
    {
        _timer += Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, _timer);
    }

    private void TryPlay()
    {
        _nextPlay++;
        if (_nextPlay >= _playRate)
        {
            _nextPlay = 0;
            Play();
        }
    }

    public void Play()
    {
        _timer = 0;
        transform.rotation = Quaternion.Euler(0, 0, _zRotation);
    }
}
