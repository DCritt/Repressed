using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private IState _currentState;

    public StateMachine(IState initState)
    {
        _currentState = initState;
        _currentState?.Enter();
    }

    public void ChangeState(IState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    public void FrameUpdate() => _currentState?.FrameUpdate();
    public void PhysicsUpdate() => _currentState?.PhysicsUpdate();
}
