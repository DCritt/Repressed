using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : Door
{
    private bool _isOpen = false;

    [SerializeField] private GameObject _door;
    private Vector3 _closedPosition;
    private Vector3 _openPosition;
    private Vector3 _targetPosition;

    private void Start()
    {
        _closedPosition = _door.transform.position;
        _openPosition = _door.transform.position + transform.TransformDirection(0, 0, -_door.GetComponent<Renderer>().bounds.size.z);
        _targetPosition = _closedPosition;
    }

    private void Update()
    {
        MoveDoor();
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

    private void MoveDoor()
    {
        _door.transform.position = Vector3.Lerp(_door.transform.position, _targetPosition, Time.deltaTime * _speed);
    }

    public override void UseDoor(Entity entity)
    {
        base.UseDoor(entity);

        _isOpen = !_isOpen;
        _targetPosition = _isOpen ? _openPosition : _closedPosition;
    }
}
