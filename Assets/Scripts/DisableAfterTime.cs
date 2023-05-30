using UnityEngine;

public class DisableAfterTime : MonoBehaviour
{
    [SerializeField] float timeToDisable;
    float timer;

    private void OnEnable()
    {
        timer = timeToDisable;
    }

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if(timer < 0f)
        {
            gameObject.SetActive(false);
        }
    }
}