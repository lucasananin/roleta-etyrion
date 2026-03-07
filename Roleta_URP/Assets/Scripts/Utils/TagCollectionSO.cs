using UnityEngine;

[CreateAssetMenu(fileName = "Tags_", menuName = "SO/Tag Collection")]
public class TagCollectionSO : ScriptableObject
{
    [SerializeField] string[] _tags = null;

    public string[] Tags { get => _tags; private set => _tags = value; }

    public bool HasTag(GameObject _gameobject)
    {
        int _count = _tags.Length;

        for (int i = 0; i < _count; i++)
        {
            if (_gameobject.CompareTag(_tags[i]))
            {
                return true;
            }
        }

        return false;
    }
}
