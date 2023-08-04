using UnityEngine;

public class ExpPickUpObject: MonoBehaviour, IPickUpObject, IPoolMember, IMovableItem
{
    private int expCount;
    private PoolMember poolMember;
    private Transform target;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    public void OnPickUp(Character character)
    {
        character.level.AddExperience(expCount);
        DestroyObj();
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if(target == null) { return; }
        Vector3 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }
    public void SetPoolMember(PoolMember poolMember)
    {
        this.poolMember = poolMember;
    }

    public void SetAmount(int amount)
    {
        expCount = amount;
    }

    private void DestroyObj()
    {
        if (poolMember == null)
        {
            Destroy(gameObject);
        }
        else
        {
            target = null;
            poolMember.ReturnToPool();
        }
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }
}