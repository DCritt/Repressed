using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _settingsMenu;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ToggleSettings()
    {
        _settingsMenu.SetActive(!_settingsMenu.activeInHierarchy);
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}