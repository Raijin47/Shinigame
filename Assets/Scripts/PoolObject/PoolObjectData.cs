using UnityEngine;

[CreateAssetMenu(fileName = "New PoolObject Data", menuName = "ScriptableObjects/PoolObjectData", order = 51)]
public class PoolObjectData : ScriptableObject
{
    public GameObject originalPrefab;
    public GameObject containerPrefab;
    public int poolID;
}