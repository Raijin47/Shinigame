using UnityEngine;

public class ProjectileSotenKisshun : ProjectileBase
{
    private float timer = 1f;
    private float curTimer;
    private Transform target;
    [SerializeField] private Vector2 radius;
    [SerializeField] private LayerMask layer;

    protected override void Move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        Turn();
        SetTarget();
    }
    private void SetTarget()
    {
        curTimer += Time.deltaTime;
        if (curTimer > timer)
        {
            curTimer = 0;
            Collider2D[] collider = Physics2D.OverlapBoxAll(EssentialService.instance.character.transform.position, radius, 0f, layer);
            if (collider.Length != 0) target = collider[Random.Range(0, collider.Length)].transform;
            else target = EssentialService.instance.character.transform;
        }
    }
    private void Turn()
    {
        if (target != null)
        {
            float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0f, 0f, angle + 90), speed/2 * Time.deltaTime);
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable enemy))
        {
            weapon.ApplyDamage(collision.transform.position, damage, enemy);
        }
        else if(collision.TryGetComponent(out Character chara))
        {
            chara.Heal(damage);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(EssentialService.instance.character.transform.position, radius);
    }
}