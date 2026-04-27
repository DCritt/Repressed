using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public interface IInteractable
{
    public InteractState InteractEnter(Entity entity)
    {
        return InteractState.Interactable;
    }
    public void Interact(Entity entity);
    public void InteractExit(Entity entity);
}
