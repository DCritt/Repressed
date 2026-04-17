using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    private bool _gamePaused = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        SetCursorLocked(!_gamePaused);
        SetTime(_gamePaused);
    }

    private void Update()
    {
    }

    public void SetGamePaused(bool paused)
    {
        _gamePaused = paused;
        SetCursorLocked(!paused);
        SetTime(paused);
        PlayerInputManager.Instance.SetMoveInputsEnabled(!paused);
    }

    private void SetCursorLocked(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }

    private void SetTime(bool paused)
    {
        Time.timeScale = paused ? 0f : 1f;
    }

}
