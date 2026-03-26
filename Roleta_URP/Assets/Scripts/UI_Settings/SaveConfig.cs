using UnityEngine;
using UnityEngine.UI;

public class SaveConfig : MonoBehaviour
{
    [SerializeField] Button _button = null;

    private void OnValidate()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Save);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Save);
    }

    private void Save()
    {
        FindFirstObjectByType<DataLoader>().Save();
    }
}
