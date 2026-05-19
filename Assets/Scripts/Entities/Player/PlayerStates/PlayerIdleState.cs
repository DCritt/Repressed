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

        if (PlayerInputManager.Instance.CrouchPressed)
        {
            if (PlayerInputManager.Instance.MovePressed)
            {
                _player.ToCrouchWalkState();
            }
            else
            {
                _player.ToCrouchIdleState();
            }
            return;
        }

        if (PlayerInputManager.Instance.MovePressed)
        {
            if (PlayerInputManager.Instance.SprintPressed)
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

    protected override void HandleInputs()
    {
        if (PlayerInputManager.Instance.JumpPressed)
        {
            _player.MovementManager.Jump();
        }
    }
}
