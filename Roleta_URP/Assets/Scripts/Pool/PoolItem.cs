using System;
using UnityEngine;

public class PoolItem : MonoBehaviour
{
    [Header("// DEBUG")]
    [SerializeField] GPTPool _pool = null;

    internal void Init(GPTPool _value)
    {
        _pool = _value;
    }

    public void Release()
    {
        _pool.ReturnObject(gameObject);
    }
}
