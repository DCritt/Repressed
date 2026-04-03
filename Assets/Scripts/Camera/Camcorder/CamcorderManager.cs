using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamcorderManager : MonoBehaviour
{
    private Player _player;

    private bool _camcorderEnabled = false;
    [SerializeField] private float _pictureCooldown;
    private float _lastPicture;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        _lastPicture = Time.time;
    }

    private void Update()
    {
        CheckForCamcorder();
        CheckForCamcorderPicture();
    }

    public void SetCamcorderEnabled(bool enabled)
    {
        _camcorderEnabled = enabled;
        PostProcessingManager.Instance.SetCamcorderEnabled(enabled);
    }

    private void CheckForCamcorder()
    {
        if (_player.InputManager.CamcorderPressed)
        {
            _camcorderEnabled = !_camcorderEnabled;
            SetCamcorderEnabled(_camcorderEnabled);
        }
    }

    private void CheckForCamcorderPicture()
    {
        float timeDelta = Time.time - _lastPicture;
        if (_camcorderEnabled && _player.InputManager.LeftMousePressed && timeDelta >= _pictureCooldown)
        {
            PostProcessingManager.Instance.TriggerCamcorderFlash();
            _lastPicture = Time.time;
        }
    }
}
