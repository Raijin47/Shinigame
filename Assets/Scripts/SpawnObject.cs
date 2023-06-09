using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject toSpawn;
    [SerializeField] [Range(0, 1f)] float probability;

    public void Spawn()
    {
        if(Random.value < probability)
        {
            SpawnManager.instance.SpawnObject(transform.position, toSpawn);
        }
    }
}