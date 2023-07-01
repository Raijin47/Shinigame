using UnityEngine;

public class EnemyFade : MonoBehaviour
{
    Material material;
    private Enemy enemy;
    int fadePropertyID;
    float fadeValue;
    [SerializeField] private float _speedFade;
    private bool isDeath = false;

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;

        fadePropertyID = Shader.PropertyToID("_DirectionalDistortionFade");

        fadeValue = 6;
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
                fadeValue = 6;
                material.SetFloat(fadePropertyID, fadeValue);
                enemy.ReturnToPool();
            }
        }
    }
}
