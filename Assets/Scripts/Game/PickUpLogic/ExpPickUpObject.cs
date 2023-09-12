using System.Collections;
using UnityEngine;

public class ExpPickUpObject: MonoBehaviour, IPickUpObject, IPoolMember, IMovableItem
{
    [SerializeField] private float speed;

    private PoolMember _poolMember;
    private Transform _target;
    private Rigidbody2D _rigidbody;
    private Vector2 _velocity;
    private int _expCount;

    private Coroutine _updateMovementCoroutine;

    public void OnPickUp(Character character)
    {
        character.level.AddExperience(_expCount);
        DestroyObj();
    }
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    public void SetPoolMember(PoolMember poolMember)
    {
        this._poolMember = poolMember;
    }
    public void SetAmount(int amount)
    {
        _expCount = amount;
    }
    public void DestroyObj()
    {
        _target = null;
        if(_updateMovementCoroutine != null)
        {
            StopCoroutine(_updateMovementCoroutine);
            _updateMovementCoroutine = null;
        }

        if (_poolMember == null)
        {
            Destroy(gameObject);
        }
        else
        {
            _poolMember.ReturnToPool();
        }
    }
    public void SetTarget(Transform t)
    {
        _target = t;

        if(gameObject.activeSelf)
        _updateMovementCoroutine = StartCoroutine(UpdateMovementProcess());
    }

    private IEnumerator UpdateMovementProcess()
    {
        while(_target != null)
        {
            _velocity = (_target.position - transform.position).normalized;
            _rigidbody.velocity = _velocity * speed;
            yield return null;
        }
    }
}