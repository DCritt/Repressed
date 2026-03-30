using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractManager: MonoBehaviour
{
    private IInteractable _currentInteractable = null;
    private Entity _entity;

    private void Awake()
    {
        _entity = GetComponent<Entity>();
    }

    public void SetInteractable(IInteractable interactable)
    {
        if (_currentInteractable == interactable) return;

        if (interactable != null) { interactable.InteractExit(_entity); }
        _currentInteractable = interactable;
        _currentInteractable.InteractEnter(_entity);
    }

    public void TryInteractable()
    {
        if (_currentInteractable == null) return;
        _currentInteractable.Interact(_entity);
    }
}