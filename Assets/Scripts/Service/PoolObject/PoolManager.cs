using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject poolPrefab;
    Dictionary<int, ObjectPool> poolList;

    private void Awake()
    {
        poolList = new Dictionary<int, ObjectPool>();
    }

    public void CreatePool(PoolObjectData newPoolData)
    {
        GameObject newObjectPoolGO = Instantiate(poolPrefab, transform).gameObject;
        ObjectPool newobjectPool = newObjectPoolGO.GetComponent<ObjectPool>();
        newobjectPool.Set(newPoolData);
        newObjectPoolGO.name = "Pool " + newPoolData.name; 
        poolList.Add(newPoolData.poolID, newobjectPool);
    }

    public GameObject GetObject(PoolObjectData poolObjectData)
    {
        if(poolList.ContainsKey(poolObjectData.poolID) == false)
        {
            CreatePool(poolObjectData);
        }

        return poolList[poolObjectData.poolID].GetObject();
    }
}
