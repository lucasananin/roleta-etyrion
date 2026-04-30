using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionConfig : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle; // optional

    private Resolution[] _resolutions;
    private List<Resolution> _uniqueResolutions = new();

    private void Start()
    {
        InitializeResolutions();
        InitializeFullscreen();
    }

    private void OnDisable()
    {
        resolutionDropdown.onValueChanged.RemoveListener(SetResolution);
        fullscreenToggle.onValueChanged.RemoveListener(SetFullscreen);
    }

    void InitializeResolutions()
    {
        _resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();
        _uniqueResolutions.Clear();

        // Remove duplicate resolutions (same width/height)
        _uniqueResolutions = _resolutions
            .GroupBy(r => new { r.width, r.height })
            .Select(g => g.First())
            .OrderBy(r => r.width)
            .ThenBy(r => r.height)
            .ToList();

        List<string> options = new();

        int currentResolutionIndex = 0;

        for (int i = 0; i < _uniqueResolutions.Count; i++)
        {
            Resolution res = _uniqueResolutions[i];

            string option = res.width + " x " + res.height;
            options.Add(option);

            if (res.width == Screen.currentResolution.width &&
                res.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    void InitializeFullscreen()
    {
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = _uniqueResolutions[resolutionIndex];

        Screen.SetResolution(
            res.width,
            res.height,
            Screen.fullScreen
        );
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
