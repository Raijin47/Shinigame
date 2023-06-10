using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTime : MonoBehaviour
{
    [HideInInspector] public float time;

    private void Update()
    {
        time += Time.deltaTime;
        EssentialService.instance.timerUI.UpdateTime(time);
    }
}