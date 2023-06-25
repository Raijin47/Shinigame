using UnityEngine;

public class ProjectileSenbonzakura: ProjectileBase
{
    private Transform target;
    [SerializeField] private Vector2 radius;

    public override void SetDirection(float dir_x, float dir_y)
    {
        
    }

    private new void Update()
    {
        Move();
        
    }

    private void SetTarget()
    {
        //Collider2D[] collider = Physics2D.OverlapBoxAll(EssentialService.instance.character.transform.position, radius);
    }
    protected override void Move()
    {
        if(target != null)
        transform.position += new Vector3(target.position.x, target.position.y) * speed * Time.deltaTime;
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