using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingDoor : Door, IInteractable
{
    private bool _isOpen = false;

    [SerializeField] private float _openAngle;
    private Quaternion _closedRotation;
    private Quaternion _targetRotation;

    private void Start()
    {
        _closedRotation = transform.rotation;
    }

    private void Update()
    {
        RotateDoor();
    }

    public override void Interact(Entity entity)
    {
        if (_lock != null)
        {
            if (_lock.CanOpen(entity))
            {
                _lock.UseLock(entity, UseDoor);
            }
            return;
        }

        UseDoor(entity);
    }

    public override InteractState InteractEnter(Entity entity)
    {
        return base.InteractEnter(entity);
    }

    public override void InteractExit(Entity entity)
    {
        
    }

    private void RotateDoor()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * _speed);
    }

    public override void UseDoor(Entity entity)
    {
        base.UseDoor(entity);

        _isOpen = !_isOpen;

        if (_isOpen)
        {
            Vector3 relativeToEntity = entity.transform.position - transform.position;
            float side = Vector3.Dot(transform.right, relativeToEntity);
            side = (side > 0) ? -1f : 1f;

            _targetRotation = Quaternion.AngleAxis(_openAngle * side, Vector3.up) * _closedRotation;
        }
        else
        {
            _targetRotation = _closedRotation;
        }
    }
}
