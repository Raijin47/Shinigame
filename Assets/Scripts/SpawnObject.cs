using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] [Range(0, 1f)] float probability;
    [SerializeField] private PoolObjectData spawnData;
    private DropManager dropManager;

    private void Start()
    {
        dropManager = EssentialService.instance.dropManager;
    }

    public void Spawn()
    {
        if(Random.value < probability)
        {
            dropManager.SpawnObj(transform.position, spawnData);
        }
    }
}