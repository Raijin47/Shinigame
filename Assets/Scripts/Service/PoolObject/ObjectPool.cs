using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private PoolObjectData _originalPoolData;
    private List<GameObject> _pool;
    public void Set(PoolObjectData pod)
    {
        _pool = new List<GameObject>();
        _originalPoolData = pod;
    }

    public void ReturnToPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public GameObject InstantiateObject()
    {
        var newObject = Instantiate(_originalPoolData.originalPrefab, transform);
        var mainObject = newObject;

        if (_originalPoolData.containerPrefab != null)
        {
            var container = Instantiate(_originalPoolData.containerPrefab);
            newObject.transform.SetParent(container.transform);
            newObject.transform.localPosition = Vector2.zero;
            mainObject = container;
        }

        _pool.Add(mainObject);
        var poolMember = mainObject.AddComponent<PoolMember>();
        poolMember.Set(this);
        return mainObject;
    }

    public GameObject GetObject()
    {
        var go = _pool.Find(e => e.activeSelf == false);
        if (go == null)
        {
            go = InstantiateObject();
        }

        go.SetActive(true);
        return go;
    }
}