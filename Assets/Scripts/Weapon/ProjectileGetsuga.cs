using UnityEngine;

public class ProjectileGetsuga : ProjectileBase
{
    public override void SetDirection(float dir_x, float dir_y)
    {
        base.SetDirection(dir_x, dir_y);
        direction = new Vector3(dir_x, dir_y);
        float angle = 0;

        switch (dir_x, dir_y)
        {
            case (1, 0):
                angle = 0;
                break;

            case ( > 0, > 0):
                angle = 45;
                break;

            case (0, 1):
                angle = 90;
                break;

            case ( < 0, > 0):
                angle = 135;
                break;

            case (-1, 0):
                angle = 180;
                break;

            case ( < 0, < 0):
                angle = 225;
                break;

            case (0, -1):
                angle = 270;
                break;

            case ( > 0, < 0):
                angle = 315;
                break;
        }

        float boostSize = EssentialService.instance.character.attackAreaSizeBonus;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localScale = new Vector2(1 * boostSize, 1 * boostSize);
    }
    protected override void DestroyProjectile() => base.DestroyProjectile();
    protected override void OnTriggerEnter2D(Collider2D collision) => base.OnTriggerEnter2D(collision);
    public override void SetPoolMember(PoolMember poolMember) => base.SetPoolMember(poolMember);
}