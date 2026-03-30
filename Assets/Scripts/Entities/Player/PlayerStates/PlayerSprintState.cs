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
        if (_player.InputManager.JumpPressed)
        {
            _player.MovementManager.Jump();
        }
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void StateChanges()
    {
        if (!_player.MovementManager.IsGrounded)
        {
            _player.ToAerialState();
            return;
        }

        if (!_player.InputManager.MovePressed)
        {
            if (_player.InputManager.CrouchPressed)
            {
                _player.ToCrouchIdleState();
            }
            else
            {
                _player.ToIdleState();
            }
            return;
        }

        if (_player.InputManager.CrouchPressed)
        {
            _player.ToCrouchWalkState();
            return;
        }

        if (!_player.InputManager.SprintPressed)
        {
            _player.ToWalkState();
            return;
        }
    }
}
