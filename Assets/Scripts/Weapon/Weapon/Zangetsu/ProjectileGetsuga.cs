﻿using UnityEngine;

public class ProjectileGetsuga : ProjectileBase
{
    private float timeScale = 1;
    private float currentTimeScale;
    public override void SetDirection(float dir_x, float dir_y)
    {
        currentTimeScale = 0;
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
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    protected override void Update()
    {
        base.Update();
        currentTimeScale += Time.deltaTime;
        if(currentTimeScale < timeScale)
            transform.localScale *= 1 + Time.deltaTime;
    }
}