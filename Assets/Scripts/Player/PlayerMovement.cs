using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private float _speed;
    private PlayerInput playerInput;
    [HideInInspector] public Rigidbody2D _rigidbody;
    private PlayerAnimate anim;
    private Vector2 movementVector;

    [HideInInspector] public float lastHorizontalDeCoupledVector;
    [HideInInspector] public float lastVerticalDeCoupledVector;
    [HideInInspector] public float lastHorizontalCoupledVector;
    [HideInInspector] public float lastVerticalCoupledVector;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        movementVector = new Vector2();
        anim = GetComponent<PlayerAnimate>();
        playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        lastHorizontalDeCoupledVector = -1f;
        lastVerticalDeCoupledVector = 1f;

        lastHorizontalCoupledVector = -1f;
        lastVerticalCoupledVector = 0;
    }
    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    void FixedUpdate()
    {
        movementVector = playerInput.direction;

        if (movementVector.x != 0 || movementVector.y != 0)
        {
            lastHorizontalCoupledVector = movementVector.x;
            lastVerticalCoupledVector = movementVector.y;
        }

        if(movementVector.x != 0)
        {
            lastHorizontalDeCoupledVector = movementVector.x;
        }
        if(movementVector.y !=0)
        {
            lastVerticalDeCoupledVector = movementVector.y;
        }

        anim.isMove = movementVector != Vector2.zero;
        anim.horizontal = movementVector.x;

        _rigidbody.velocity = movementVector.normalized * _speed;
    }
}