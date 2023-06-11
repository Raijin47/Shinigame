using UnityEngine;

public enum PlatformInput
{
    Keyboard,
    Joystick
}

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public Vector2 direction;
    private PlatformInput state;

    private void Start()
    {
        state = PlatformInput.Keyboard;
    }
    void Update()
    {
        switch (state)
        {
            case PlatformInput.Keyboard:
                direction.x = Input.GetAxisRaw("Horizontal");
                direction.y = Input.GetAxisRaw("Vertical");
                break;
            case PlatformInput.Joystick:

                break;
        }

    }
}