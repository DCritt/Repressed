using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public interface IInteractable
{
    public void InteractEnter(Entity entity);
    public void Interact(Entity entity);
    public void InteractExit(Entity entity);
}
