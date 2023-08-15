using UnityEngine;

public class ProjectileKunai : ProjectileBase
{
    public override void SetDirection(float dir_x, float dir_y)
    {
        base.SetDirection(dir_x, dir_y);

        direction = new Vector2(dir_x, dir_y);
        if(direction == Vector3.zero) direction = new Vector2(1, 0);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, angle - 180);
    }
}