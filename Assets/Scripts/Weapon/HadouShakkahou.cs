using UnityEngine;

public class HadouShakkahou : MonoBehaviour
{
    [SerializeField] float timeToAttack;
    float timer;

    PlayerMovement playerMove;

    [SerializeField] GameObject hadouPrefab;


    private void Awake()
    {
        playerMove = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        if (timer < timeToAttack)
        {
            timer += Time.deltaTime;
            return;
        }

        timer = 0;
        SpawnHadou();
    }

    private void SpawnHadou()
    {
        GameObject shakkahou = Instantiate(hadouPrefab);
        shakkahou.transform.position = transform.position;
        shakkahou.GetComponent<HadouShakkahouProjectile>().SetDirection(playerMove.lastHorizontalVector, playerMove.lastVerticalVector);
    }
}
