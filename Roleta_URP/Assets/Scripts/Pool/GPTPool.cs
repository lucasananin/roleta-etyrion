using System.Collections.Generic;
using UnityEngine;

public class GPTPool : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] GameObject _prefab = null;
    [SerializeField] int poolSize = 10;
    [SerializeField] bool expandable = true;

    //private readonly Queue<GameObject> poolQueue = new();
    [Header("// DEBUG")]
    [SerializeField] List<GameObject> _poolList = new();
    [SerializeField] List<GameObject> _activePoolList = new();

    private void Awake()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            CreateObject();
        }
    }

    private GameObject CreateObject()
    {
        var _obj = Instantiate(_prefab, transform);
        _obj.GetComponent<PoolItem>().Init(this);
        _obj.SetActive(false);
        //poolQueue.Enqueue(_obj);
        _poolList.Add(_obj);
        return _obj;
    }

    public GameObject GetObject()
    {
        if (_poolList.Count == 0)
        {
            if (expandable)
            {
                return CreateObject();
            }
            else
            {
                return null;
            }
        }

        //var _obj = poolQueue.Dequeue();
        var _obj = _poolList[^1];
        _poolList.Remove(_obj);
        _activePoolList.Add(_obj);
        _obj.SetActive(true);
        return _obj;
    }

    public void ReturnObject(GameObject _obj)
    {
        _obj.SetActive(false);
        _obj.transform.SetParent(transform);
        //poolQueue.Enqueue(_obj);
        _poolList.Add(_obj);
        _activePoolList.Remove(_obj);
    }

    public void ReturnAll()
    {
        int _count = _activePoolList.Count;
        for (int i = _count - 1; i >= 0; i--)
            ReturnObject(_activePoolList[i]);
    }
}
