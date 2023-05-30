using UnityEngine;

public class HadouShakkahouProjectile : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] int damage;
    bool hitDetected = false;
    public void SetDirection(float dir_x, float dir_y)
    {
        direction = new Vector3(dir_x, dir_y);
        if (dir_x < 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = scale.x * -1;
            transform.localScale = scale;
        }
    }
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Time.frameCount % 6 == 0)// ןנמגונךא םא ךאזהûי 2י פנויל
        {
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 0.3f);
            foreach (Collider2D c in hit)
            {
                Enemy enemy = c.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    hitDetected = true;
                    break;
                }
            }
            if (hitDetected == true)
            {
                Destroy(gameObject);
            }
        }
    }
}