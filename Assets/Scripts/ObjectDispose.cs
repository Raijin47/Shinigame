using UnityEngine;

public class ObjectDispose : MonoBehaviour
{
    //[SerializeField] float maxDistance;
    //Transform playerTransform;

    //private void Start()
    //{
    //    playerTransform = GameManager.instance.playerTransform;
    //}
    //private void Update()
    //{
    //    float distance = Vector3.Distance(transform.position, playerTransform.position);
    //    if(distance > maxDistance)
    //    {
    //        gameObject.GetComponent<PoolMember>().ReturnToPool();
    //    }
    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PoolMember obj))
        {
            obj.ReturnToPool();
        }
    }
}