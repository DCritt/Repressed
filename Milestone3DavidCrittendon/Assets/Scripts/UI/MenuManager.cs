using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private GameObject _photoMenu;

    private GameObject _activeMenu = null;

    private void Update()
    {
        CheckForPauseMenuPress();
        CheckForPhotoMenuPress();
    }

    private void CheckForPauseMenuPress()
    {
        if (GlobalInputManager.Instance.PausePressed)
        {
            SetActiveMenu(_pauseMenu);
        }
    }

    private void CheckForPhotoMenuPress()
    {
        if (GlobalInputManager.Instance.PhotoMenuPressed)
        {
            SetActiveMenu(_photoMenu);
        }
    }

    public void SetActiveMenu(GameObject menu)
    {
        _activeMenu?.SetActive(false);
        if (menu != _activeMenu)
        {
            _activeMenu = menu;
            _activeMenu.SetActive(true);
        }
        else
        {
            _activeMenu = null;
        }

        PauseManager.Instance.SetGamePaused(_activeMenu ? true : false);
    }

    public void NavigateMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
