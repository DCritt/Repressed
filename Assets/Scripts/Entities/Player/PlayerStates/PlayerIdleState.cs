using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player) : base(player)
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

        if (_player.InputManager.CrouchPressed)
        {
            if (_player.InputManager.MovePressed)
            {
                _player.ToCrouchWalkState();
            }
            else
            {
                _player.ToCrouchIdleState();
            }
            return;
        }

        if (_player.InputManager.MovePressed)
        {
            if (_player.InputManager.SprintPressed)
            {
                _player.ToSprintState();
            }
            else
            {
                _player.ToWalkState();
            }
            return;
        }
    }
}
