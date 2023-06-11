using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float smooth;
    [SerializeField] private float offset;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x, player.position.y + offset, transform.position.z), smooth);
    }
}