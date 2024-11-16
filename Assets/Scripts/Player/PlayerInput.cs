using UnityEngine;
using UnityEngine.UI;
using YG;
public enum PlatformInput
{
    Keyboard,
    Joystick
}

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private VariableJoystick joystick;
    [HideInInspector] public Vector2 direction;
    [SerializeField] private PlatformInput state;

    private readonly string Joystick = "JoysticType";

    private void OnEnable() => YandexGame.GetDataEvent += GetData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetData;
    private void Awake()
    {
        if (YandexGame.SDKEnabled == true)
        {
            GetData();
        }
    }
    public void GetData()
    {
        if (YandexGame.EnvironmentData.isDesktop)
        {
            joystick.GetComponent<Image>().raycastTarget = false;
            state = PlatformInput.Keyboard;
        }
        else
        {
            state = PlatformInput.Joystick;
            joystick.SetMode((JoystickType)PlayerPrefs.GetInt(Joystick, 1));
        }
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
                direction = joystick.Direction;
                break;
        }
    }
}