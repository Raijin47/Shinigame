using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    private void Start()
    {
        UnPauseGame();
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        playerInput.StopAction();
    }
    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        playerInput.ResumeAction();
    }
}