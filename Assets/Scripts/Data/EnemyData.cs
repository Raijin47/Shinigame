using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "ScriptableObjects/EnemyData", order = 51)]
public class EnemyData : ScriptableObject
{
    public string nameEnemy;
    public PoolObjectData poolObjectData;
    public EnemyStats stats;
}