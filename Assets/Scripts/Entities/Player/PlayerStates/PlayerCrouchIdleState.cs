using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerState
{
    private float _crouchIdleMoveMult = 0f;
    private float _crouchIdleAccMult = 1f;

    public PlayerCrouchIdleState(Player player) : base(player)
    {
    }

    public override void FrameUpdate()
    {
        if (_player.JumpPressed())
        {
            _player.Jump();
        }
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        _player.Move(_crouchIdleMoveMult, _crouchIdleAccMult);
        base.PhysicsUpdate();
    }

    public override void StateChanges()
    {
        if (!_player.IsGrounded())
        {
            _player.ToAerialState();
            return;
        }

        if (!_player.CrouchPressed())
        {
            if (!_player.MovePressed())
            {
                _player.ToIdleState();
            }
            else if (_player.SprintPressed())
            {
                _player.ToSprintState();
            }
            else
            {
                _player.ToWalkState();
            }
            return;
        }

        if (_player.MovePressed())
        {
            _player.ToCrouchWalkState();
            return;
        }
    }
}
