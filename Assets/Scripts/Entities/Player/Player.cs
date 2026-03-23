using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class Player : Entity
{
    private InputManager _inputManager;
    private MovementManager _movementManager;

    private PlayerIdleState _idleState;
    private PlayerCrouchIdleState _crouchIdleState;
    private PlayerCrouchWalkState _crouchWalkState;
    private PlayerWalkState _walkState;
    private PlayerSprintState _sprintState;
    private PlayerAerialState _aerialState;
    private StateMachine _stateMachine;

    void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _movementManager = GetComponent<MovementManager>();

        _idleState = new PlayerIdleState(this); 
        _crouchIdleState = new PlayerCrouchIdleState(this);
        _crouchWalkState = new PlayerCrouchWalkState(this);
        _walkState = new PlayerWalkState(this);
        _sprintState = new PlayerSprintState(this);
        _aerialState = new PlayerAerialState(this);
        _stateMachine = new StateMachine(_idleState);
    }

    void Update()
    {
        _stateMachine.FrameUpdate();
    }

    void FixedUpdate()
    {
        _stateMachine.PhysicsUpdate();
    }

    public Vector2 MovementInput() => _inputManager.MovementInput;
    public bool MovePressed() => _inputManager.MovePressed;
    public bool JumpPressed() => _inputManager.JumpPressed;
    public bool CrouchPressed() => _inputManager.CrouchPressed;
    public bool SprintPressed() => _inputManager.SprintPressed;

    public void Move(float stateMove, float accMult)
    {
        _movementManager.Move(_inputManager.MovementInput, stateMove, accMult);
    }
    public void Jump()
    {
        _movementManager.Jump();
    }
    public bool IsGrounded() => _movementManager.IsGrounded();

    public void ToIdleState()
    {
        _stateMachine.ChangeState(_idleState);
    }
    public void ToCrouchIdleState()
    {
        _stateMachine.ChangeState(_crouchIdleState);
    }
    public void ToCrouchWalkState()
    {
        _stateMachine.ChangeState(_crouchWalkState);
    }
    public void ToWalkState()
    {
        _stateMachine.ChangeState(_walkState);
    }
    public void ToSprintState()
    {
        _stateMachine.ChangeState(_sprintState);
    }
    public void ToAerialState()
    {
        _stateMachine.ChangeState(_aerialState);
    }
}