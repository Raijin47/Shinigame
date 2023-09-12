using UnityEngine;
using YG;

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
        YandexGame.StickyAdActivity(true);
        _isForced = isForced;
        _playerInput.StopAction();
        Time.timeScale = 0f;
    }
    public void UnPauseGame(bool isForced)
    {
        YandexGame.StickyAdActivity(false);
        _isForced = isForced;
        _playerInput.ResumeAction();
        Time.timeScale = 1f;
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