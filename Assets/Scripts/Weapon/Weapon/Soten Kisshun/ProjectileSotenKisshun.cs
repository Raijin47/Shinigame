using UnityEngine;

public class ProjectileSotenKisshun : ProjectileBase
{
    [SerializeField] private float maxDistance;
    private float timer = .5f;
    private float curTimer;
    private Character chara;

    private void Start()
    {
        chara = EssentialService.instance.character;
    }
    public override void SetDirection(float dir_x, float dir_y)
    {
        base.SetDirection(dir_x, dir_y);
        direction = new Vector3(dir_x, dir_y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, angle + 90);
    }
    protected override void Update()
    {
        base.Update();
        NewPosition();
    }
    private void NewPosition()
    {
        if(Vector2.Distance(transform.position, weapon.transform.position) > maxDistance)
        {
            curTimer += Time.deltaTime;
            if(curTimer > timer)
            {
                curTimer = 0;
                direction = UtilityTools.GenerateOppositePattern(direction).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.localRotation = Quaternion.Euler(0f, 0f, angle + 90);
            }
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable enemy = collision.GetComponent<IDamageable>();

        if (collision.TryGetComponent(out IDamageable _))
        {
            weapon.ApplyDamage(collision.transform.position, damage, enemy);
        }
        else if(collision.TryGetComponent(out Character _))
        {
            chara.Heal(damage);
        }
    }
}