using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAerialState : PlayerState
{
    private float _aerialMoveMult = 1f;
    private float _aerialAccMult = 0.1f;

    public PlayerAerialState(Player player) : base(player)
    {
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        _player.Move(_aerialMoveMult, _aerialAccMult);
        base.PhysicsUpdate();
    }

    public override void StateChanges()
    {
        if (_player.IsGrounded())
        {
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
            }
            else
            {
                _player.ToIdleState();
            }
            return;
        }
    }
}
