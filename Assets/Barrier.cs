using UnityEngine;

public class Barrier: MonoBehaviour
{
    [SerializeField] private Vector2 _inNormal;
    [SerializeField] private Vector2 _forceVector;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out EnemyProjectile projectile))
        {
            projectile.ReboundDirection(_inNormal);
        }
        if(collision.TryGetComponent(out PlayerMovement player))
        {
            player._rigidbody.AddForce(_forceVector, ForceMode2D.Force);
            player.GetComponent<Character>().TakeDamage(10);
        }
    }
}