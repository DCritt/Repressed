using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : IState
{
    protected Player _player;
    
    public PlayerState(Player player)
    {
        _player = player;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void FrameUpdate() 
    {
        StateChanges();
    }
    public virtual void PhysicsUpdate() { }

    protected abstract void StateChanges();
    protected abstract void HandleInputs();
}