using UnityEngine;
public interface IDamageable
{
    public void TakeDamage(int dagame);
    public void Knockback(Vector2 vector, float force, float timeWeight);
    void Stun(float stun);
}