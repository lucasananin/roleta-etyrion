using UnityEngine;
using UnityEngine.Pool;

public class SimplePool : MonoBehaviour
{
    [SerializeField] GameObject _prefab = null;

    // The pool holds plain GameObjects (you can swap this for any component type).
    private ObjectPool<GameObject> pool;

    public ObjectPool<GameObject> Pool { get => pool; }

    void Awake()
    {
        // Create a pool with the four core callbacks.
        pool = new ObjectPool<GameObject>(
            createFunc: CreateItem,
            actionOnGet: OnGet,
            actionOnRelease: OnRelease,
            actionOnDestroy: OnDestroyItem,
            collectionCheck: true,   // helps catch double-release mistakes
            defaultCapacity: 10,
            maxSize: 50
        );
    }

    //void Update()
    //{
    //    // Press Space to spawn one pooled object for 1 second.
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        GameObject gameObject = pool.Get();
    //        gameObject.transform.position = Random.insideUnitSphere * 5f;

    //        // Return it to the pool after a short delay.
    //        StartCoroutine(ReturnAfter(gameObject, 1f));
    //    }
    //}

    // Creates a new pooled GameObject the first time (and whenever the pool needs more).
    private GameObject CreateItem()
    {
        //GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //gameObject.name = "PooledCube";
        var gameObject = Instantiate(_prefab, Vector3.zero, Quaternion.identity, transform);
        //gameObject.GetComponent<PoolItem>().Init(this);
        gameObject.SetActive(false);
        return gameObject;
    }

    // Called when an item is taken from the pool.
    private void OnGet(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    // Called when an item is returned to the pool.
    private void OnRelease(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    // Called when the pool decides to destroy an item (e.g., above max size).
    private void OnDestroyItem(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    private System.Collections.IEnumerator ReturnAfter(GameObject gameObject, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        // Give it back to the pool.
        pool.Release(gameObject);
    }
}
