using System.Collections;
using UnityEngine;

public class EnemyMenos : Enemy
{
    [SerializeField] private float _offsetTarget;
    [SerializeField] private float _timeCeroRotate;
    [SerializeField] private float _timeCeroAttack;
    private float _timeDelayOutPhase = 1f;
    private float _timeDelayPhase = 5f;

    private ParticleSystem _particle;
    private Transform _transformParticle;
    private Transform _target;
    private Coroutine _updatePhaseCoroutine;


    private bool _isAction;

    public override void Activate()
    {
        base.Activate();

        _target = GetComponentInChildren<FindService>().transform;
        
        _particle = GetComponentInChildren<ParticleSystem>();
        _transformParticle = _particle.transform;

        if(_updatePhaseCoroutine != null)
        {
            StopCoroutine(_updatePhaseCoroutine);
            _updatePhaseCoroutine = null;              
        }
        _updatePhaseCoroutine = StartCoroutine(UpdatePhaseProcess());
    }

    private IEnumerator UpdatePhaseProcess()
    {
        while (_isActive)
        {
            _isAction = true;
            _rigidbody.velocity = Vector2.zero;

            var action = Random.Range(0, 2) switch
            {
                0 => CeroPhase(),
                1 => IdlePhase(),
                _ => null,
            };
            yield return StartCoroutine(action);
            _isAction = false;
            yield return new WaitForSeconds(_timeDelayPhase);
        }
    }
    private IEnumerator CeroPhase()
    {
        if(_isRight) _transformParticle.position = transform.position + new Vector3(0.5f,1.85f);
        else _particle.transform.position = transform.position + new Vector3(-0.5f, 1.85f);
        _particle.Play();

        float t = 0;

        while (t < _timeCeroRotate)
        {
            float angle = Mathf.Atan2(_targetDestination.position.y + _offsetTarget - _transformParticle.position.y, _targetDestination.position.x - _transformParticle.position.x) * Mathf.Rad2Deg;
            _transformParticle.localRotation = Quaternion.Lerp(_transformParticle.localRotation, Quaternion.Euler(0f, 0f, angle), Time.deltaTime);
            t += Time.deltaTime;
            yield return null;
        }

        t = 0;

        while (t < _timeCeroAttack)
        {
            Collider2D col = Physics2D.Linecast(_transformParticle.position, _target.position, _player).collider;
            if (col != null)
            {
                _targetCharacter.TakeDamage(Stats.Damage);
            }
            t += Time.deltaTime;
            yield return new WaitForSeconds(_timeToAttack);
        }

        _particle.Stop();

        yield return new WaitForSeconds(_timeDelayOutPhase);
    }
    private IEnumerator IdlePhase()
    {
        yield return null;
    }

    protected override void Defeated()
    {
        base.Defeated();
        _updatePhaseCoroutine = null;
    }
    protected override void Flip()
    {
        if (_isAction) { return; }
        base.Flip();
    }
    protected override void Move()
    {
        if (_isAction) { return; }
        base.Move();
    }
    public override void Knockback(Vector2 vector, float force, float timeWeight)
    {

    }
}