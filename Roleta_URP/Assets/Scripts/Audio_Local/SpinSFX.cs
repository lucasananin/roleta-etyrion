using UnityEngine;

public class SpinSFX : AudioPlayer
{
    [SerializeField] RouletteWheel _wheel = null;

    private float _nextPlay = 0f;

    private void LateUpdate()
    {
        if (_wheel.Spinning)
        {
            _nextPlay += Time.deltaTime;

            if (_nextPlay > _wheel.TimeBetweenSlots)
            {
                _nextPlay = 0;
                Play();
            }
        }
    }
}
