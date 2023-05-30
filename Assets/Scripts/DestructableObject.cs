using UnityEngine;

public class DestructableObject : MonoBehaviour, IDamageable
{
    public void TakeDamage(int damage)
    {
        GetComponent<DropOnDestroy>().CheckDrop();
        Destroy(gameObject);
    }
}