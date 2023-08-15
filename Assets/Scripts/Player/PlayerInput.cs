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
    [SerializeField] private Joystick joystick;
    [HideInInspector] public Vector2 direction;
    [SerializeField] private PlatformInput state;

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
            state = PlatformInput.Keyboard;
        }
        else
        {
            state = PlatformInput.Joystick;
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
    public void StopAction() => joystick.GetComponent<Image>().raycastTarget = false;
    public void ResumeAction() => joystick.GetComponent<Image>().raycastTarget = true;
}