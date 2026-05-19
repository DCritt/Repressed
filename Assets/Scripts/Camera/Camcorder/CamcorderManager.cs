using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CamcorderManager : MonoBehaviour
{
    private Player _player;

    [SerializeField] private Camera _photoCamera;
    [SerializeField] private RenderTexture _photoRenderTexture;

    private bool _camcorderEnabled = false;
    [SerializeField] private float _pictureCooldown;
    private float _lastPicture;
    [SerializeField] private float _zoomTargetSpeed;
    [SerializeField] private float _zoomSpeed;
    private float _camcorderZoom = 0f;
    private float _camcorderTargetZoom = 0f;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _photoCamera.targetTexture = _photoRenderTexture;
    }

    private void Start()
    {
        _lastPicture = Time.time;
    }

    private void Update()
    {
        CheckForCamcorder();
        CheckForCamcorderPicture();
        CheckForCamcorderZoom();
    }

    public void SetCamcorderEnabled(bool enabled)
    {
        _camcorderEnabled = enabled;
        PostProcessingManager.Instance.SetCamcorderEnabled(enabled);
        _camcorderZoom = 0f;
        _camcorderTargetZoom = 0f;
        _photoCamera.enabled = enabled;
    }

    private void CheckForCamcorder()
    {
        if (PlayerInputManager.Instance.CamcorderPressed)
        {
            _camcorderEnabled = !_camcorderEnabled;
            SetCamcorderEnabled(_camcorderEnabled);
            _player.PlayerUIManager.SetCamcorderUIEnabled(_camcorderEnabled);
        }
    }

    private void CheckForCamcorderPicture()
    {
        float timeDelta = Time.time - _lastPicture;
        if (_camcorderEnabled && PlayerInputManager.Instance.LeftMousePressed && timeDelta >= _pictureCooldown)
        {
            CapturePicture();
            PostProcessingManager.Instance.TriggerCamcorderFlash();
            _lastPicture = Time.time;
        }
    }

    private void CapturePicture()
    {
        RenderTexture prev = RenderTexture.active;
        RenderTexture.active = _photoRenderTexture;

        Texture2D picture = new Texture2D(_photoRenderTexture.width, _photoRenderTexture.height, TextureFormat.RGBA32, false, true);
        picture.ReadPixels(new Rect(0, 0, _photoRenderTexture.width, _photoRenderTexture.height), 0, 0);
        picture.Apply();

        RenderTexture.active = prev;

        SaveManager.Instance.SavePicture(picture);
    }

    private void CheckForCamcorderZoom()
    {
        float input = PlayerInputManager.Instance.MouseWheelInput;
        if (_camcorderEnabled)
        {
            _camcorderTargetZoom += input * _zoomTargetSpeed * Time.deltaTime;
            _camcorderTargetZoom = Mathf.Clamp01(_camcorderTargetZoom);
            _camcorderZoom = Mathf.Lerp(_camcorderZoom, _camcorderTargetZoom, _zoomSpeed * Time.deltaTime);
        }
        PostProcessingManager.Instance.SetCamcorderZoomScale(_camcorderZoom);
    }
}
