using UnityEngine;

public class DropManager: MonoBehaviour
{
    [SerializeField] private PoolManager poolManager;
    [SerializeField] private PoolObjectData expPrefab;

    public void ExpDrop(Vector2 position, int amount)
    {
        GameObject expDrop = poolManager.GetObject(expPrefab);
        expDrop.transform.position = position;
        ExpPickUpObject exp = expDrop.GetComponent<ExpPickUpObject>();
        exp.SetAmount(amount);
    }
    public void Drop(Vector2 position, PoolObjectData[] drop, float chance)
    {
        if(Random.value < chance)
        {
            GameObject toDrop = poolManager.GetObject(drop[Random.Range(0, drop.Length)]);
            toDrop.transform.position = position;
        }
    }
    public void SpawnObj(Vector2 position, PoolObjectData data)
    {
        GameObject obj = poolManager.GetObject(data);
        obj.transform.position = position;
    }
}