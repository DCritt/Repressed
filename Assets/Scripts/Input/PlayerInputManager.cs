using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance { get; private set; }
    private bool _moveInputsEnabled = true;

    public Vector2 MovementInput { get; private set; }
    public bool MovePressed { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool CrouchPressed { get; private set; }
    public bool SprintPressed { get; private set; }
    public bool InteractPressed { get; private set; }
    public bool CamcorderPressed { get; private set; }
    public bool LeftMousePressed { get; private set; }

    public float MouseInputX { get; private set; }
    public float MouseInputY { get; private set; }
    public float MouseWheelInput {  get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    void Update()
    {
        if (_moveInputsEnabled)
        {
            MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            MovePressed = MovementInput.magnitude > 0;
            JumpPressed = Input.GetKeyDown(KeyCode.Space);
            CrouchPressed = Input.GetKey(KeyCode.LeftControl);
            SprintPressed = Input.GetKey(KeyCode.LeftShift);
            InteractPressed = Input.GetKeyDown(KeyCode.E);
            CamcorderPressed = Input.GetKeyDown(KeyCode.C);
            LeftMousePressed = Input.GetMouseButtonDown(0);

            MouseInputX = Input.GetAxisRaw("Mouse X");
            MouseInputY = Input.GetAxisRaw("Mouse Y");
            MouseWheelInput = Input.GetAxisRaw("Mouse ScrollWheel");
        }
    }

    public void SetMoveInputsEnabled(bool enabled)
    {
        _moveInputsEnabled = enabled;
        MovementInput = Vector2.zero;
        MovePressed = false;
        JumpPressed = false;
        CrouchPressed = false;
        SprintPressed = false;
        InteractPressed = false;
        CamcorderPressed = false;
        LeftMousePressed = false;

        MouseInputX = 0;
        MouseInputY = 0;
    }
}