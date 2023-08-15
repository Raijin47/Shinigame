using UnityEngine;

public class ProjectileGetsuga : ProjectileBase
{
    private float timeScale = 1;
    private float currentTimeScale;
    public override void SetDirection(float dir_x, float dir_y)
    {
        currentTimeScale = 0;
        base.SetDirection(dir_x, dir_y);

        direction = new Vector3(dir_x, dir_y);
        if (direction == Vector3.zero) direction = new Vector2(1, 0);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    protected override void Update()
    {
        base.Update();
        currentTimeScale += Time.deltaTime;
        if(currentTimeScale < timeScale)
            transform.localScale *= 1 + Time.deltaTime;
    }
}