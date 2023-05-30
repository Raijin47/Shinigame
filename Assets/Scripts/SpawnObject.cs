using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] GameObject toSpawn;
    [SerializeField] [Range(0, 1f)] float probability;

    public void Spawn()
    {
        if(Random.value < probability)
        {
            GameObject go = Instantiate(toSpawn, transform.position, Quaternion.identity);
        }
    }
}