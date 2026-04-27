using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] private GameObject _camcorderUIPanel;
    [SerializeField] private Image _crosshairImage;

    [SerializeField] private Sprite _crosshair;
    [SerializeField] private Sprite _interactCrosshair;
    [SerializeField] private Sprite _interactLockcrosshair;

    public void SetInteractCrosshair(InteractState interactState)
    {
        if (interactState == InteractState.None)
        {
            _crosshairImage.sprite = _crosshair;
        }
        else if (interactState == InteractState.Interactable)
        {
            _crosshairImage.sprite = _interactCrosshair;
        }
        else
        {
            _crosshairImage.sprite = _interactLockcrosshair;
        }
    }

    public void SetCamcorderUIEnabled(bool enabled)
    {
        _camcorderUIPanel.SetActive(enabled);
    }
}
