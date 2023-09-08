using UnityEngine;

public class ObjectDispose : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IPickUpObject obj))
        {
            obj.DestroyObj();
        }
    }
}