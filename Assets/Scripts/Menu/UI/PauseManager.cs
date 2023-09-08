using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _pausePanel;
    private bool _isForced;

    private void Start()
    {
        UnPauseGame(false);
        _isForced = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!_isForced)
            {
                if (!_pausePanel.activeInHierarchy) OpenMenu();
                else CloseMenu();
            }
        }
    }
    public void PauseGame(bool isForced)
    {
        _isForced = isForced;
        Time.timeScale = 0f;
        _playerInput.StopAction();
    }
    public void UnPauseGame(bool isForced)
    {
        _isForced = isForced;
        Time.timeScale = 1f;
        _playerInput.ResumeAction();
    }

    public void CloseMenu()
    {
        UnPauseGame(false);
        _pausePanel.SetActive(false);
    }

    public void OpenMenu()
    {
        PauseGame(false);
        _pausePanel.SetActive(true);
    }
}