using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    [HideInInspector] public float horizontal;

    private Animator anim;

    void Update()
    {
        anim.SetFloat("Horizontal", horizontal);
    }

    internal void SetAnimate(GameObject animObject)
    {
        anim = animObject.GetComponent<Animator>();
    }
}