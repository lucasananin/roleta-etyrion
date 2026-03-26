using UnityEngine;
using UnityEngine.Events;

public class DataLoader : MonoBehaviour
{
    [SerializeField] GameDataSO _defaultSO = null;
    [SerializeField] GameDataSO _runtimeSO = null;

    public static event UnityAction OnRestored = null;
    public static event UnityAction OnLoaded = null;

    private void Start()
    {
        Load();
    }

    private void Load()
    {
        var _p = FindFirstObjectByType<PersistenceHandler>().LoadData();

        if (_p == null)
        {
            RestoreDefault();
        }
        else
        {
            _runtimeSO.NumberOfSlots = _p.numberOfSlots;
            _runtimeSO.DurationRange = _p.durationRange;
            _runtimeSO.SpeedRange = _p.speedRange;
            OnLoaded?.Invoke();
        }
    }

    internal void RestoreDefault()
    {
        _runtimeSO.NumberOfSlots = _defaultSO.NumberOfSlots;
        _runtimeSO.DurationRange = _defaultSO.DurationRange;
        _runtimeSO.SpeedRange = _defaultSO.SpeedRange;
        OnRestored?.Invoke();
    }

    internal void Save()
    {
        FindFirstObjectByType<PersistenceHandler>().SaveData(_runtimeSO);
    }
}
