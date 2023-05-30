using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    Transform targetDestination;
    [SerializeField] float _speed;
    [SerializeField] int hp;
    [SerializeField] int _damage;
    [SerializeField] int experienceReward;
    Rigidbody2D rb;
    GameObject targetGameObject;
    Character targetCharacter;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;
    }
    void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rb.velocity = direction * _speed;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameObject)
        {
            Attack();
        }
    }

    void Attack()
    {
        if(targetCharacter == null)
        {
            targetCharacter = targetGameObject.GetComponent<Character>();
        }
        targetCharacter.TakeDamage(_damage);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 1)
        {
            targetGameObject.GetComponent<Level>().AddExperience(experienceReward);
            GetComponent<DropOnDestroy>().CheckDrop();
            Destroy(gameObject);
        }
    }
}
