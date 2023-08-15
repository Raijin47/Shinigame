using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    [HideInInspector] public float horizontal;
    public bool isMove;
    private readonly string moveAnim = "isMove";
    private Animator anim;

    void Update()
    {
        anim.SetBool(moveAnim, isMove);
        anim.SetFloat("Horizontal", horizontal);
    }

    internal void SetAnimate(GameObject animObject)
    {
        anim = animObject.GetComponent<Animator>();
    }
}