using UnityEngine;

public class ProjectileKunai : ProjectileBase
{
    [SerializeField] private SpriteRenderer sprite;
    public override void SetDirection(float dir_x, float dir_y)
    {
        base.SetDirection(dir_x, dir_y);

        direction = new Vector2(dir_x, 0f);
        if(dir_x > 0)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }
    }
}