using UnityEngine;

public class AutoPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IMovableItem item))
        {
            item.SetTarget(transform);
        }
    }
}