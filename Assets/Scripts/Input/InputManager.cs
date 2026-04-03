using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
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

    void Update()
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
    }
}
