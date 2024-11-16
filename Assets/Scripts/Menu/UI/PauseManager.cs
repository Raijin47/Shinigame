using System.Collections;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _boostPanel;
    private bool _isForced;

    private void Start()
    {
        UnPauseGame(false);
        StartCoroutine(BoostCoroutine());
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
    }
    public void UnPauseGame(bool isForced)
    {
        _isForced = isForced;
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

    private IEnumerator BoostCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        _boostPanel.SetActive(true);
        PauseGame(true);
    }
}