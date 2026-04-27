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
        IInteractable interactable = null;
        if (hit.collider != null)
        {
            interactable = hit.distance <= _interactDistance ? hit.collider.GetComponentInParent<IInteractable>() : null;
        }
        _player.InteractManager.SetInteractable(interactable);

        _player.PlayerUIManager.SetInteractCrosshair(_player.InteractManager.InteractState);
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
