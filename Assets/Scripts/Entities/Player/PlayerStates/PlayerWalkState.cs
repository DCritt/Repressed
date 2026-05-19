using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    public PlayerWalkState(Player player) : base(player)
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
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        HandleInputs();
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

        if (PlayerInputManager.Instance.SprintPressed)
        {
            _player.ToSprintState();
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
