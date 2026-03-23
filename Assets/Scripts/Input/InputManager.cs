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

    void Update()
    {
        MovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        MovePressed = MovementInput.magnitude > 0;
        JumpPressed = Input.GetKeyDown(KeyCode.Space);
        CrouchPressed = Input.GetKey(KeyCode.LeftControl);
        SprintPressed = Input.GetKey(KeyCode.LeftShift);
    }
}
