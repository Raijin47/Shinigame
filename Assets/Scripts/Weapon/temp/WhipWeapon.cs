using System.Collections;
using UnityEngine;

public class WhipWeapon : WeaponBase
{
    [SerializeField] GameObject leftWhipObject;
    [SerializeField] GameObject rightWhipObject;

    [SerializeField] Vector2 attackSize;

    public override void Attack()
    {
        StartCoroutine(AttackProcess());
    }

    IEnumerator AttackProcess()
    {
        for(int i = 0; i < weaponStats.numberOfAttacks; i++)
        {
            if (playerMove.lastHorizontalDeCoupledVector > 0)
            {
                rightWhipObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(rightWhipObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
            }
            else
            {
                leftWhipObject.SetActive(true);
                Collider2D[] colliders = Physics2D.OverlapBoxAll(leftWhipObject.transform.position, attackSize, 0f);
                ApplyDamage(colliders);
            }
        }
        yield return new WaitForSeconds(0.3f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(leftWhipObject.transform.position, attackSize);
    }
}