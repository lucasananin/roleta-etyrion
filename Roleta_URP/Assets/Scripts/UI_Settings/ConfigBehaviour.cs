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
    }

    protected void OnDisable()
    {
        DataLoader.OnRestored -= UpdateVisuals;
    }

    public abstract void UpdateVisuals();
}
