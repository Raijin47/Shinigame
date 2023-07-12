using UnityEngine;

public class ProjectileHyorinmaru : ProjectileBase
{
    public override void SetDirection(float dir_x, float dir_y)
    {
        base.SetDirection(dir_x, dir_y);
        direction = new Vector3(dir_x, dir_y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, angle + 90);
    }
    protected override void Update()
    {
        TimerToLive();
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