using UnityEngine;

public class DestructableObject : MonoBehaviour, IDamageable
{
    public void Knockback(Vector2 vector, float force, float time)
    {

    }

    public void Stun(float stun)
    {

    }

    public void TakeDamage(int damage)
    {
        GetComponent<DropOnDestroy>().CheckDrop();
        Destroy(gameObject);
    }
}