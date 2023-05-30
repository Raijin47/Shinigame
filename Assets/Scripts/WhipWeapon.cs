using UnityEngine;

public class WhipWeapon : MonoBehaviour
{
    [SerializeField] float timeToAttack;
    float timer;

    [SerializeField] GameObject leftWhipObject;
    [SerializeField] GameObject rightWhipObject;
    [SerializeField] int _damage;
    PlayerMovement playerMove;
    [SerializeField] Vector2 whipAttackSize;

    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMovement>();
    }
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0f)
        {
            Attack();
        }
        
    }
    private void Attack()
    {
        timer = timeToAttack;

        if(playerMove.lastHorizontalVector > 0)
        {
            rightWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, whipAttackSize, 0f);
            ApplyDamage(colliders);
        }
        else
        {
            leftWhipObject.SetActive(true);
            Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, whipAttackSize, 0f);
            ApplyDamage(colliders);
        }
    }

    void ApplyDamage(Collider2D[] colliders)
    {
        for(int i = 0; i < colliders.Length; i++)
        {
            IDamageable e = colliders[i].GetComponent<IDamageable>();
            if(e != null)
            {
                e.TakeDamage(_damage);
            }
        }
    }
}