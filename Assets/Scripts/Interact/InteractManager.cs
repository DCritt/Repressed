using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager: MonoBehaviour
{
    public InteractState InteractState { get; private set; } = InteractState.None;
    private IInteractable _currentInteractable = null;
    private Entity _entity;

    private void Awake()
    {
        _entity = GetComponent<Entity>();
    }

    public void SetInteractable(IInteractable interactable)
    {
        if (_currentInteractable == interactable) return;

        InteractState = InteractState.None;
        if (_currentInteractable != null) { _currentInteractable.InteractExit(_entity); }
        _currentInteractable = interactable;
        if (interactable != null) { InteractState = _currentInteractable.InteractEnter(_entity); }
    }

    public void TryInteractable()
    {
        if (_currentInteractable == null) return;
        _currentInteractable.Interact(_entity);
    }
}