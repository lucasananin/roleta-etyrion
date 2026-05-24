using UnityEngine;

public class SpinSFX : AudioPlayer
{
    [SerializeField] RouletteWheel _wheel = null;
    [SerializeField] int _playRate = 2;

    private int _nextPlay = 0;

    private void OnEnable()
    {
        _wheel.OnSlotChanged += TryPlay;
    }

    private void OnDisable()
    {
        _wheel.OnSlotChanged -= TryPlay;
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
}
