using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchWalkState : PlayerState
{
    private string _speedMultName = "CrouchWalk";
    private float _crouchSpeedMult = 0.5f;

    public PlayerCrouchWalkState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.MovementManager.ApplySpeedMult(_speedMultName, _crouchSpeedMult);
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

        if (!_player.InputManager.CrouchPressed)
        {
            if (!_player.InputManager.MovePressed)
            {
                _player.ToIdleState();
            }
            else if (_player.InputManager.SprintPressed) 
            {
                _player.ToSprintState();
            }
            else
            {
                _player.ToWalkState();
            }
            return;
        }

        if (!_player.InputManager.MovePressed)
        {
            _player.ToCrouchIdleState();
            return;
        }
    }
}
