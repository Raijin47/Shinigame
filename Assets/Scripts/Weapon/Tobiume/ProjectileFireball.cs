using UnityEngine;

public class ProjectileFireball : ProjectileBase
{
    public override void SetDirection(float dir_x, float dir_y)
    {
        base.SetDirection(dir_x, dir_y);
        direction = new Vector3(dir_x, dir_y).normalized;
    }
}