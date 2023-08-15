using UnityEngine;

public class PoolMember : MonoBehaviour
{
    private ObjectPool _pool;

    public void Set(ObjectPool pool)
    {
        _pool = pool;
        GetComponent<IPoolMember>().SetPoolMember(this);
    }

    public void ReturnToPool()
    {
        _pool.ReturnToPool(gameObject);
    }
}