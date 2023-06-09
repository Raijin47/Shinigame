using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void SpawnObject(Vector2 worldPosition, GameObject toSpawn)
    {
        Transform t = Instantiate(toSpawn, transform).transform;
        t.position = worldPosition;
    }
}
