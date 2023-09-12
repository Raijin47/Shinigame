using UnityEngine;

public class JoystickSettings : MonoBehaviour
{
    [SerializeField] private Animator[] buttons;
    private int currentID = -1;
    private readonly string Select = "Highlighted";
    private readonly string Deselect = "Normal";

    private readonly string Joystick = "JoysticType";

    private void Start()
    {
        currentID = PlayerPrefs.GetInt(Joystick, 1);
        buttons[currentID].Play(Select);
    }
    public void SetType(int id)
    {
        if(currentID == id) { return; }

        buttons[currentID].Play(Deselect);

        currentID = id;

        buttons[currentID].Play(Select);

        PlayerPrefs.SetInt(Joystick, currentID);
    }
}