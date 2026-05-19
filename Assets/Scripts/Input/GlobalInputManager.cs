using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalInputManager : MonoBehaviour
{
    public static GlobalInputManager Instance { get; private set; }
    
    public bool PausePressed { get; private set; }
    public bool PhotoMenuPressed { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Update()
    {
        PausePressed = Input.GetKeyDown(KeyCode.Escape);
        PhotoMenuPressed = Input.GetKeyDown(KeyCode.Tab);
    }
}
