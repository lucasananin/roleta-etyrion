using UnityEngine;

public class DataLoader : MonoBehaviour
{
    [SerializeField] GameDataSO _defaultSO = null;
    [SerializeField] GameDataSO _runtimeSO = null;

    private void Start()
    {
        // load using persistence system.

        _runtimeSO.NumberOfSlots = _defaultSO.NumberOfSlots;
        _runtimeSO.DurationRange = _defaultSO.DurationRange;
        _runtimeSO.SpeedRange = _defaultSO.SpeedRange;
    }
}
