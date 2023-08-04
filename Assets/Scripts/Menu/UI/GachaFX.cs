using UnityEngine;

public class GachaFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private GachaPanel gachaPanel;

    public void Rewarded() => gachaPanel.Reward();
    public void ParticleStopped() => particle.Stop();
    public void ParticlePlay() => particle.Play();
}