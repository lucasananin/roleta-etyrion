using UnityEngine;
using UnityEngine.UI;

public class RestoreDefaultConfig : MonoBehaviour
{
    [SerializeField] Button _button = null;

    private void OnValidate()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Restore);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Restore);
    }

    private void Restore()
    {
        FindFirstObjectByType<DataLoader>().RestoreDefault();
    }
}
