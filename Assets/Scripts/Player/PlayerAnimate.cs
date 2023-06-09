using UnityEngine;

public class PlayerAnimate : MonoBehaviour
{
    [HideInInspector] public float horizontal;

    Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        anim.SetFloat("Horizontal", horizontal);
    }
}