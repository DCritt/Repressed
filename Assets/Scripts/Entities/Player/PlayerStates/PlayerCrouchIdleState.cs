using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerState
{
    public PlayerCrouchIdleState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
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

        if (_player.InputManager.MovePressed)
        {
            _player.ToCrouchWalkState();
            return;
        }
    }
}
