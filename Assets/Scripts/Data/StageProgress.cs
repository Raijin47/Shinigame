using UnityEngine;

public class StageProgress : MonoBehaviour
{
    [SerializeField] private float progressTimeRate;
    [SerializeField] private float progressPerSplit;
    StageTime stageTime;

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