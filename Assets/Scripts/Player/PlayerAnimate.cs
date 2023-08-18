using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    private Animator anim;
    private Transform player;

    [HideInInspector] public float horizontal;
    [HideInInspector] public bool isMove;

    private bool _isLeft;

    private readonly string moveAnim = "isMove";
    void Update()
    {
        anim.SetBool(moveAnim, isMove);
        Flip();
    }
    private void Flip()
    {
        if(horizontal != 0)
        {
            var isLeft = horizontal > 0;
            if (_isLeft != isLeft)
            {
                _isLeft = !_isLeft;
                player.localScale = new Vector2(_isLeft ? -1 : 1, 1);
            }
        }
    }
    internal void SetAnimate(GameObject animObject)
    {
        player = animObject.transform;
        anim = animObject.GetComponent<Animator>();
    }
}