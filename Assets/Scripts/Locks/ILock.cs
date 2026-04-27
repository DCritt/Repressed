using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface ILock
{
    public bool CanOpen(Entity entity);
    public void UseLock(Entity entity, UnityAction<Entity> useDoor);
}
