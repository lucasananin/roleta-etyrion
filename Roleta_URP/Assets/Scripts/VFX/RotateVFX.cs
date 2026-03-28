using UnityEngine;

public class RotateVFX : MonoBehaviour
{
    [SerializeField] Transform _t = null;
    [SerializeField] float _spinsPerSecond = 1f;

    private void LateUpdate()
    {
        _t.Rotate(0, 0, (_spinsPerSecond * 360) * Time.deltaTime);
    }
}
