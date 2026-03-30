using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAerialState : PlayerState
{
    private string _accMultName = "Aerial";
    private float _aerialAccMult = 0.1f;

    public PlayerAerialState(Player player) : base(player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.MovementManager.ApplyAccMult(_accMultName, _aerialAccMult);
    }

    public override void Exit()
    {
        base.Exit();
        _player.MovementManager.RemoveAccMult(_accMultName);
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void StateChanges()
    {
        if (_player.MovementManager.IsGrounded)
        {
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
            }
            else
            {
                _player.ToIdleState();
            }
            return;
        }
    }
}
