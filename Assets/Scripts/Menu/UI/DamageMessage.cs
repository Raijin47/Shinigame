using UnityEngine;

public class DamageMessage : MonoBehaviour
{
    [SerializeField] private float timeToLive = 2f;
    private RectTransform _transform;
    private float ttl;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        ttl = timeToLive;
    }

    private void Update()
    {
        _transform.Translate(Vector3.up * Time.deltaTime);
        ttl -= Time.deltaTime;
        if (ttl < 0f)
        {
            gameObject.SetActive(false);
        }
    }
}