using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Door : MonoBehaviour, IInteractable
{
    [SerializeField] protected float _speed;
    [SerializeField] protected MonoBehaviour _lockScript;
    protected ILock _lock;

    private void Awake()
    {
        _lock = _lockScript as ILock;
    }

    public abstract void Interact(Entity entity);

    public virtual InteractState InteractEnter(Entity entity)
    {
        return (_lock == null || _lock.CanOpen(entity)) ? InteractState.Interactable : InteractState.Uninteractable;
    }

    public virtual void InteractExit(Entity entity)
    {

    }

    public virtual void UseDoor(Entity entity)
    {
        if (_lock != null) _lock = null;
    }
}
