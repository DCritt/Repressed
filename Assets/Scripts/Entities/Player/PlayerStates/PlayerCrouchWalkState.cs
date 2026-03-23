using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchWalkState : PlayerState
{
    private float _crouchMoveMult = 0.5f;
    private float _crouchAccMult = 1f;

    public PlayerCrouchWalkState(Player player) : base(player)
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
        if (_player.JumpPressed())
        {
            _player.Jump();
        }
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        _player.Move(_crouchMoveMult, _crouchAccMult);
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

        if (!_player.MovePressed())
        {
            _player.ToCrouchIdleState();
            return;
        }
    }
}
