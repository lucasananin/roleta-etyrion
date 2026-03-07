using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] string _sceneToLoad = null;
    [SerializeField] string _sceneToUnload = null;
    [SerializeField] bool _loadOnStart = false;

    public static event UnityAction<string, string> OnLoadScene = null;

    private IEnumerator Start()
    {
        yield return null;

        if (_loadOnStart)
        {
            Load();
        }
    }

    public virtual void Load()
    {
        OnLoadScene?.Invoke(_sceneToLoad, _sceneToUnload);
    }

    public void SetValues(string _sceneToLoad, string _sceneToUnload)
    {
        this._sceneToLoad = _sceneToLoad;
        this._sceneToUnload = _sceneToUnload;
    }
}
