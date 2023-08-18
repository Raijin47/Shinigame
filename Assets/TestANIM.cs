using UnityEngine;

public class TestANIM : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private readonly string IsMove = "isMove";

    private bool isRun;
    public void Swap()
    {
        isRun = !isRun;

        anim.SetBool(IsMove, isRun);
    }
}