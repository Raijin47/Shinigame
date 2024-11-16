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
    public DropManager dropManager;
    public PoolManager poolManager;
    public EnemyManager enemyManager;
    public FinalStatictic finalStatictic;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
}