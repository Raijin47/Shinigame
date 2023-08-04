using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Character targetCharacter;
    private GameObject targetGameObject;
    private float timeToAttack = .5f;
    private float currentTime;
    private bool isAttack = false;
    public int damage;

    void Start()
    {
        
    }
    void Update()
    {
        ProcessAttack();
    }
    private void ProcessAttack()
    {
        currentTime -= Time.deltaTime;
        if (isAttack && currentTime < 0)
        {
            Attack();
            currentTime = timeToAttack;
        }
    }
    void Attack()
    {
        if (targetCharacter == null)
        {
            targetCharacter = targetGameObject.GetComponent<Character>();
        }
        targetCharacter.TakeDamage(damage);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameObject)
        {
            isAttack = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameObject)
        {
            isAttack = false;
        }
    }
}
