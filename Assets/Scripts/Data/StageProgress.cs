using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageProgress : MonoBehaviour
{
    StageTime stageTime;
    [SerializeField] private float progressTimeRate;
    [SerializeField] private float progressPerSplit;
    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
    }

    public float Progress
    {
        get
        {
            return 1f + stageTime.time / progressTimeRate * progressPerSplit;
        }
    }
}
