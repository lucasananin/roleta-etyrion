using UnityEngine;

public abstract class ConfigBehaviour : MonoBehaviour
{
    [SerializeField] protected GameDataSO _runtimeSO = null;

    protected void Awake()
    {
        UpdateVisuals();
    }

    protected void OnEnable()
    {
        DataLoader.OnRestored += UpdateVisuals;
        DataLoader.OnLoaded += UpdateVisuals;
    }

    protected void OnDisable()
    {
        DataLoader.OnRestored -= UpdateVisuals;
        DataLoader.OnLoaded -= UpdateVisuals;
    }

    public abstract void UpdateVisuals();
}
