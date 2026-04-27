using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NonvisibleObjectSwapper : MonoBehaviour
{
    [SerializeField] private GameObject _toBeSwapped;
    [SerializeField] private GameObject _toBeSwappedTo;
    private Renderer[] renderers1;
    private Renderer[] renderers2;
    [SerializeField] private Camera _playerCam;
    private bool _swapped = false;
    private bool _enabled = false;

    private void Awake()
    {
        if (_toBeSwapped != null) 
        {
            renderers1 = _toBeSwapped.GetComponentsInChildren<Renderer>();
        }
        else
        {
            renderers1 = new Renderer[0]; 
        }

        if (_toBeSwappedTo != null) 
        {
            renderers2 = _toBeSwappedTo.GetComponentsInChildren<Renderer>();
        }
        else
        {
            renderers2 = new Renderer[0];
        }
    }

    private void Update()
    {
        TestSwap();
    }

    private void TestSwap()
    {
        if (_swapped) return;

        if (!IsVisible() && _enabled)
        {
            if (_toBeSwapped != null) { _toBeSwapped?.SetActive(false); }
            if (_toBeSwappedTo != null) { _toBeSwappedTo?.SetActive(true); }
            _swapped = true;
        }
    }

    private bool IsVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_playerCam);

        foreach (Renderer renderer in renderers1)
        {
            if (GeometryUtility.TestPlanesAABB(planes, renderer.bounds))
            {
                return true;
            }
        }

        foreach (Renderer renderer in renderers2)
        {
            if (GeometryUtility.TestPlanesAABB(planes, renderer.bounds))
            {
                return true;
            }
        }

        return false;
    }

    public void SetEnabled(bool enabled)
    {
        _enabled = enabled;
    }
}
