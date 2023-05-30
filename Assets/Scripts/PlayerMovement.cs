using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _speed;

    Rigidbody2D rb;
    Vector3 movementVector;
    PlayerAnimate anim;

    [HideInInspector] public float lastHorizontalVector;
    [HideInInspector] public float lastVerticalVector;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        anim = GetComponent<PlayerAnimate>();
    }
    private void Start()
    {
        lastHorizontalVector = 1f;
        lastVerticalVector = 1f;
    }
    void FixedUpdate()
    {
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");

        if(movementVector.x != 0)
        {
            lastHorizontalVector = movementVector.x;
        }
        if(movementVector.y !=0)
        {
            lastVerticalVector = movementVector.y;
        }

        anim.horizontal = movementVector.x; 

        movementVector *= _speed;

        rb.velocity = movementVector;
    }
}