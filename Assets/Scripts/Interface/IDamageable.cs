using UnityEngine;
public interface IDamageable
{
    public void TakeDamage(int dagame, bool showMessage = true);
    public void Knockback(Vector2 vector, float force, float timeWeight);
    void Stun(float stun);

    public void Burn(float time, int damage);
}