using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintState : PlayerState
{
    private float _sprintMoveMult = 2f;
    private float _sprintAccMult = 1f;

    public PlayerSprintState(Player player) : base(player)
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
        _player.Move(_sprintMoveMult, _sprintAccMult);
        base.PhysicsUpdate();
    }

    public override void StateChanges()
    {
        if (!_player.IsGrounded())
        {
            _player.ToAerialState();
            return;
        }

        if (!_player.MovePressed())
        {
            if (_player.CrouchPressed())
            {
                _player.ToCrouchIdleState();
            }
            else
            {
                _player.ToIdleState();
            }
            return;
        }

        if (_player.CrouchPressed())
        {
            _player.ToCrouchWalkState();
            return;
        }

        if (!_player.SprintPressed())
        {
            _player.ToWalkState();
            return;
        }
    }
}
