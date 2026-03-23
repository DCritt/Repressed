using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    private float _idleMoveMult = 1f;
    private float _idleAccMult = 1f;

    public PlayerIdleState(Player player) : base(player)
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
        base.PhysicsUpdate();
        _player.Move(_idleMoveMult, _idleAccMult);
    }

    public override void StateChanges()
    {
        if (!_player.IsGrounded())
        {
            _player.ToAerialState();
            return;
        }

        if (_player.CrouchPressed())
        {
            if (_player.MovePressed())
            {
                _player.ToCrouchWalkState();
            }
            else
            {
                _player.ToCrouchIdleState();
            }
            return;
        }

        if (_player.MovePressed())
        {
            if (_player.SprintPressed())
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
