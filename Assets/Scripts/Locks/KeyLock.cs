using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyLock : MonoBehaviour, ILock
{
    [SerializeField] private string _keyId;

    public bool CanOpen(Entity entity)
    {
        IKeyHolder keyHolder;

        if (entity.TryGetComponent<IKeyHolder>(out keyHolder))
        {
            return keyHolder.HasKey(_keyId);
        }
        return false;
    }

    public void UseLock(Entity entity, UnityAction<Entity> useDoor)
    {
        useDoor(entity);
        Destroy(this);
    }
}
