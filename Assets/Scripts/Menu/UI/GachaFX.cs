using UnityEngine;

public class GachaFX : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private GachaPanel gachaPanel;
    [SerializeField] private GameObject button;
    public void Twist()
    {
        particle.Play();
        anim.SetTrigger("twist");
    }
    public void Rewarded()
    {
        gachaPanel.Reward();
    }
    public void StopTwist()
    {
        particle.Stop();
        anim.SetTrigger("stop");
        button.SetActive(true);
    }
}