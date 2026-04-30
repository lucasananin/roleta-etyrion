using UnityEngine;
using UnityEngine.UI;

public class AudioConfig : MonoBehaviour
{
    [Header("Optional UI")]
    [SerializeField] Toggle _audioToggle;

    private void Start()
    {
        // Initialize toggle state based on current volume
        if (_audioToggle != null)
        {
            _audioToggle.isOn = AudioListener.volume > 0f;
            _audioToggle.onValueChanged.AddListener(SetAudio);
        }
    }

    private void OnDisable()
    {
        _audioToggle.onValueChanged.RemoveListener(SetAudio);
    }

    /// <summary>
    /// Enable or disable all audio
    /// </summary>
    public void SetAudio(bool enabled)
    {
        AudioListener.volume = enabled ? 1f : 0f;
    }
}
