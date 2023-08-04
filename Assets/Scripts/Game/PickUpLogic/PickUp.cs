using UnityEngine;

public class PickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Character chara))
        {
            GetComponent<IPickUpObject>().OnPickUp(chara);
        }
    }
}