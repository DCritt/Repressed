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

    protected override void StateChanges()
    {
        if (_player.MovementManager.IsGrounded)
        {
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
            }
            else
            {
                _player.ToIdleState();
            }
            return;
        }
    }

    protected override void HandleInputs()
    {
        
    }
}
