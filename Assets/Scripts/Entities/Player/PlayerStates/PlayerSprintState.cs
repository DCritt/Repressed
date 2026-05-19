using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintState : PlayerState
{
    private string _speedMultName = "Sprint";
    private float _sprintSpeedMult = 2f;

    public PlayerSprintState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.MovementManager.ApplySpeedMult(_speedMultName, _sprintSpeedMult);
    }

    public override void Exit()
    {
        base.Exit();
        _player.MovementManager.RemoveSpeedMult(_speedMultName);
    }

    public override void FrameUpdate()
    {
        HandleInputs();
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected override void StateChanges()
    {
        if (!_player.MovementManager.IsGrounded)
        {
            _player.ToAerialState();
            return;
        }

        if (!PlayerInputManager.Instance.MovePressed)
        {
            if (PlayerInputManager.Instance.CrouchPressed)
            {
                _player.ToCrouchIdleState();
            }
            else
            {
                _player.ToIdleState();
            }
            return;
        }

        if (PlayerInputManager.Instance.CrouchPressed)
        {
            _player.ToCrouchWalkState();
            return;
        }

        if (!PlayerInputManager.Instance.SprintPressed)
        {
            _player.ToWalkState();
            return;
        }
    }

    protected override void HandleInputs()
    {
        if (PlayerInputManager.Instance.JumpPressed)
        {
            _player.MovementManager.Jump();
        }
    }
}
