using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractManager : MonoBehaviour
{
    private Player _player;
    [SerializeField] private float _interactDistance;

    private bool _interactEnabled = true;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        CheckForInteract();
    }

    private void FixedUpdate()
    {
        CheckForInteractable();
    }

    public void SetInteractEnabled(bool enabled)
    {
        _interactEnabled = enabled;
    }

    private void CheckForInteractable()
    {
        RaycastHit hit = _player.CameraManager.SendRaycast();
        IInteractable interactable;
        if (hit.collider != null && hit.transform.TryGetComponent<IInteractable>(out interactable))
        {
            _player.InteractManager.SetInteractable(interactable);
        }
    }

    private void CheckForInteract()
    {
        if (!_interactEnabled) { return; }
        if (PlayerInputManager.Instance.InteractPressed)
        {
            _player.InteractManager.TryInteractable();
        }
    }
}
