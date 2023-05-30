using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smooth;
    [SerializeField] float offset;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, player.position.y + offset, transform.position.z), smooth);
    }
}