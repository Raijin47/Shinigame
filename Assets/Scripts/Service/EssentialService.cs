using UnityEngine;

public class EssentialService : MonoBehaviour
{
    public static EssentialService instance;
    public TimerUI timerUI;
    public PauseManager pauseManager;
    public PlayerWinManager playerWinManager;
    public DataContainer dataContainer;
    public Character character;
    public MessageSystem message;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}