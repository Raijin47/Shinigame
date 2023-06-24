using UnityEngine;


public class SenbonzakuraProjectile : ProjectileBase
{
    private Transform target;

    private void Update()
    {
        Move();
    }
    protected override void Move()
    {
        transform.position += new Vector3(target.position.x, target.position.y) * speed * Time.deltaTime;
    } 

    public void GetTarget(Collider2D getTarget)
    {
        target = getTarget.transform;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable enemy = collision.GetComponent<IDamageable>();

        if (collision.TryGetComponent(out IDamageable _))
        {
            weapon.ApplyDamage(collision.transform.position, damage, enemy);
        }
    }
}
