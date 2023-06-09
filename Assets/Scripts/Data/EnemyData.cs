using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public string nameEnemy;
    public GameObject animatedPrefab;
    public EnemyStats stats;
}
