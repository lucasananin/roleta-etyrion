using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] float _loadTimeOffset = 0.5f;

    private WaitForSeconds _waitTime = null;

    public static event UnityAction OnStartLoading = null;
    public static event UnityAction OnEndLoading = null;

    private void Awake()
    {
        _waitTime = new WaitForSeconds(_loadTimeOffset);
    }

    private void OnEnable()
    {
        SceneLoader.OnLoadScene += LoadScene;
    }

    private void OnDisable()
    {
        SceneLoader.OnLoadScene -= LoadScene;
    }

    public void LoadScene(string _sceneToLoad, string _sceneToUnload)
    {
        StartCoroutine(LoadScene_Routine(_sceneToLoad, _sceneToUnload));
    }

    private IEnumerator LoadScene_Routine(string _sceneToLoad, string _sceneToUnload)
    {
        Time.timeScale = 1;
        OnStartLoading?.Invoke();
        yield return _waitTime;

        if (!string.IsNullOrEmpty(_sceneToUnload))
        {
            yield return SceneManager.UnloadSceneAsync(_sceneToUnload);
        }

        yield return SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneToLoad));

        yield return _waitTime;
        OnEndLoading?.Invoke();
    }
}
