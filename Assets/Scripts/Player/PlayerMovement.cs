using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed;

    Rigidbody2D rb;
    Vector3 movementVector;
    PlayerAnimate anim;

    [HideInInspector] public float lastHorizontalDeCoupledVector;
    [HideInInspector] public float lastVerticalDeCoupledVector;


    [HideInInspector] public float lastHorizontalCoupledVector;
    [HideInInspector] public float lastVerticalCoupledVector;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        anim = GetComponent<PlayerAnimate>();
    }
    private void Start()
    {
        lastHorizontalDeCoupledVector = 1f;
        lastVerticalDeCoupledVector = 1f;

        lastHorizontalCoupledVector = 1f;
        lastVerticalCoupledVector = 0;
    }
    void FixedUpdate()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");


        if(movementVector.x != 0 || movementVector.y != 0)
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

        anim.horizontal = movementVector.x; 

        movementVector *= _speed;

        rb.velocity = movementVector;
    }
}