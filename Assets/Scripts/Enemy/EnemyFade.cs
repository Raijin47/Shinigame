using System.Collections;
using UnityEngine;

public class EnemyFade : MonoBehaviour
{
    [SerializeField] private float _speedFade;

    SpriteRenderer _renderer;
    MaterialPropertyBlock _block;
    MaterialPropertyBlock _blockBurning;
    Enemy _enemy;

    float _burnValue;
    float _fadeValue;

    int fadePropertyID;
    int firePropertyID;


    void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
        _fadeValue = 1;
        _renderer = GetComponent<SpriteRenderer>();
        _block = new MaterialPropertyBlock();
        _blockBurning = new MaterialPropertyBlock();

        fadePropertyID = Shader.PropertyToID("_FullGlowDissolveFade");
        firePropertyID = Shader.PropertyToID("_BurnFade");
    }
    public void Death() => StartCoroutine(DeathCOR());
    public void Fire(bool isBurn)
    {
        _burnValue = isBurn ? 1 : 0;
        _blockBurning?.SetFloat(firePropertyID, _burnValue);
        _renderer?.SetPropertyBlock(_blockBurning);
    }
    IEnumerator DeathCOR()
    {
        _fadeValue = 1;
        while (_fadeValue > 0)
        {
            _fadeValue -= Time.deltaTime * _speedFade;
            _block.SetFloat(fadePropertyID, _fadeValue);
            _renderer.SetPropertyBlock(_block);

            if (_fadeValue <= 0)
            {
                _fadeValue = 1;
                _block.SetFloat(fadePropertyID, _fadeValue);
                _renderer.SetPropertyBlock(_block);
                _enemy.ReturnToPool();
                yield break;
            }
            yield return null;
        }
    }
}