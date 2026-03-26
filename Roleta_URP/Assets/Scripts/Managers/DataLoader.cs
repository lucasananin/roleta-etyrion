using UnityEngine;
using UnityEngine.Events;

public class DataLoader : MonoBehaviour
{
    [SerializeField] GameDataSO _defaultSO = null;
    [SerializeField] GameDataSO _runtimeSO = null;

    public static event UnityAction OnRestored = null;

    private void Start()
    {
        // load using persistence system.
        RestoreDefault();
    }

    internal void RestoreDefault()
    {
        _runtimeSO.NumberOfSlots = _defaultSO.NumberOfSlots;
        _runtimeSO.DurationRange = _defaultSO.DurationRange;
        _runtimeSO.SpeedRange = _defaultSO.SpeedRange;
        OnRestored?.Invoke();
    }
}
