using UnityEngine;

public class EnemyFade : MonoBehaviour
{
    Material material;
    private Enemy enemy;
    int fadePropertyID;
    int fireID;
    float fadeValue;
    float burnValue;
    [SerializeField] private float _speedFade;
    private bool isDeath = false;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;

        fadePropertyID = Shader.PropertyToID("_FullGlowDissolveFade");
        fireID = Shader.PropertyToID("_BurnFade");
        fadeValue = 1;
        burnValue = 0;
        enemy = GetComponentInParent<Enemy>();
    }

    public void Death(bool death)
    {
        isDeath = death;
    }
    void Update()
    {
        if(isDeath)
        {
            fadeValue -= Time.deltaTime * _speedFade;
            material.SetFloat(fadePropertyID, fadeValue);
            if (fadeValue < 0) 
            {
                fadeValue = 1;
                material.SetFloat(fadePropertyID, fadeValue);
                enemy.ReturnToPool();
            }
        }
    }
    public void Fire(bool isBurn)
    {
        burnValue = isBurn ? 1 : 0;
        material.SetFloat(fireID, burnValue);
    }
}