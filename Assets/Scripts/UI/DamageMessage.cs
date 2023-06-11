using UnityEngine;

public class DamageMessage : MonoBehaviour
{
    [SerializeField] private float timeToLive = 2f;
    private float ttl = 2f;
    
    private void OnEnable()
    {
        ttl = timeToLive;
    }

    private void Update()
    {
        ttl -= Time.deltaTime;
        if(ttl < 0f)
        {
            gameObject.SetActive(false);
        }
    }
}