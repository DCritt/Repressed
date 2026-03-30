using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    public InputManager InputManager { get; private set; }
    public MovementManager MovementManager { get; private set; }
    public PlayerInteractManager PlayerInteractManager { get; private set; }
    public InteractManager InteractManager { get; private set; }
    [field: SerializeField] public CameraManager CameraManager { get; private set; }
    [field: SerializeField] public Follow Facade { get; private set; }

    private PlayerIdleState _idleState;
    private PlayerCrouchIdleState _crouchIdleState;
    private PlayerCrouchWalkState _crouchWalkState;
    private PlayerWalkState _walkState;
    private PlayerSprintState _sprintState;
    private PlayerAerialState _aerialState;
    private StateMachine _stateMachine;

    void Awake()
    {
        InputManager = GetComponent<InputManager>();
        MovementManager = GetComponent<MovementManager>();
        PlayerInteractManager = GetComponent<PlayerInteractManager>();
        InteractManager = GetComponent<InteractManager>();

        _idleState = new PlayerIdleState(this); 
        _crouchIdleState = new PlayerCrouchIdleState(this);
        _crouchWalkState = new PlayerCrouchWalkState(this);
        _walkState = new PlayerWalkState(this);
        _sprintState = new PlayerSprintState(this);
        _aerialState = new PlayerAerialState(this);
        _stateMachine = new StateMachine(_idleState);
    }

    private void Start()
    {
        CameraManager.SetTarget(transform);
        Facade.SetTarget(transform);
    }

    void Update()
    {
        _stateMachine.FrameUpdate();
        CameraManager.UpdateInput(InputManager.MouseInputX, -InputManager.MouseInputY);
        MovementManager.SetMoveInput(InputManager.MovementInput);
        MovementManager.SetRotation(CameraManager.Yaw);
    }

    void FixedUpdate()
    {
        _stateMachine.PhysicsUpdate();
    }

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