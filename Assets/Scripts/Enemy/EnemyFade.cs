using System.Collections;
using UnityEngine;
using DragonBones;

public class EnemyFade : MonoBehaviour
{
    [SerializeField] private float _speedFade;

    MeshRenderer _renderer;
    MaterialPropertyBlock _block;
    MaterialPropertyBlock _blockBurning;
    Enemy _enemy;
    UnityArmatureComponent _component;

    float _burnValue;
    float _fadeValue;

    int fadePropertyID;
    int firePropertyID;

    readonly string Run = "run";

    void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
        _fadeValue = 1;
        _renderer = GetComponentInChildren<MeshRenderer>();
        _block = new MaterialPropertyBlock();
        _blockBurning = new MaterialPropertyBlock();
        _component = GetComponent<UnityArmatureComponent>();

        _component.animation.Play(Run);

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
        while (true)
        {
            _fadeValue -= Time.deltaTime * _speedFade;
            _block.SetFloat(fadePropertyID, _fadeValue);
            _renderer.SetPropertyBlock(_block);

            if (_fadeValue < 0)
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