using UnityEngine;

public class ProjectileSenbonzakura: ProjectileBase
{
    public Transform target;
    [SerializeField] private Vector2 radius;
    [SerializeField] private LayerMask layer;
    private float ttgt = 2f;
    private float cttht;
    private float tta = .1f;
    private float ctta;
    [SerializeField]private float attackSize;

    private new void Update()
    {
        base.Update();
        SetTarget();
        Attack();
    }
    private void SetTarget()
    {
        cttht -= Time.deltaTime;
        if (cttht < 0)
        {
            Collider2D[] collider = Physics2D.OverlapBoxAll(EssentialService.instance.character.transform.position, radius, 0f, layer);
            if (collider.Length != 0) target = collider[Random.Range(0, collider.Length)].transform;
            cttht = ttgt;
        }
    }
    private void Attack()
    {
        ctta -= Time.deltaTime;
        if(ctta < 0)
        {
            ctta = tta;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackSize);
            weapon.ApplyDamage(colliders);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        IDamageable enemy = collision.GetComponent<IDamageable>();

        if (collision.TryGetComponent(out IDamageable _))
        {
            weapon.ApplyDamage(collision.transform.position, damage, enemy);
        }
    }
    protected override void Move()
    {
        if (target != null)
            transform.position = Vector2.Lerp(transform.position, new Vector2(target.position.x, target.position.y), speed * Time.deltaTime);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {


    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackSize);
    }

}