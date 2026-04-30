using UnityEngine;
using UnityEngine.UI;

public class VSyncConfig : MonoBehaviour
{
    [Header("Optional UI")]
    [SerializeField] Toggle _vsyncToggle;

    private void Start()
    {
        // Initialize toggle state based on current VSync setting
        if (_vsyncToggle != null)
        {
            _vsyncToggle.isOn = (QualitySettings.vSyncCount > 0);
            _vsyncToggle.onValueChanged.AddListener(SetVSync);
        }
    }

    private void OnDisable()
    {
        _vsyncToggle.onValueChanged.RemoveListener(SetVSync);
    }

    /// <summary>
    /// Enable or disable VSync
    /// </summary>
    public void SetVSync(bool enabled)
    {
        if (enabled)
        {
            QualitySettings.vSyncCount = 1; // Sync every vertical blank
            Application.targetFrameRate = -1; // Let VSync control FPS
        }
        else
        {
            QualitySettings.vSyncCount = 0; // Disable VSync
            Application.targetFrameRate = 60; // Optional fallback cap
        }
    }
}
