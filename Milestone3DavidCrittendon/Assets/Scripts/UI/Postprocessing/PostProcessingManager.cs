using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager Instance { get; private set; }

    [SerializeField] private Material _camcorderScanlineMat;
    private string _camcorderShaderWeight = "_Shader_Weight";
    private float _targetScanlineIntensity = 0f;

    [SerializeField] private Volume _camcorderVolume;
    private LensDistortion _camcorderLensDistortion;

    [SerializeField] private Volume _camcorderFlashVolume;
    private ColorAdjustments _flashColor;
    private Bloom _flashBloom;
    private List<Coroutine> _flashCoroutines = new();

    [SerializeField] private float _blendSpeed;
    [SerializeField] private float _flashEffectLength;
    [SerializeField] private float _flashFadeLength;

    private float _camcorderTargetWeight = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    private void Start()
    {
        _camcorderVolume.profile.TryGet<LensDistortion>(out _camcorderLensDistortion);
        _camcorderFlashVolume.profile.TryGet<ColorAdjustments>(out _flashColor);
        _camcorderFlashVolume.profile.TryGet<Bloom>(out _flashBloom);

        SetCamcorderEnabled(false);
    }

    private void Update()
    {
        UpdateCamcorderVolumeWeight();
        UpdateScanlineIntensity();
    }

    public void SetCamcorderEnabled(bool enabled)
    {
        _camcorderTargetWeight = enabled ? 1f : 0f;
        _targetScanlineIntensity = enabled ? 1f : 0f;
    }

    private void UpdateCamcorderVolumeWeight()
    {
        _camcorderVolume.weight = Mathf.Lerp(_camcorderVolume.weight, _camcorderTargetWeight, _blendSpeed * Time.deltaTime);
        if (_camcorderVolume.weight < 0.01f && _camcorderTargetWeight == 0f)
        {
            _camcorderVolume.weight = 0f;
        }
    }

    private void UpdateScanlineIntensity()
    {
        float value = _camcorderScanlineMat.GetFloat(_camcorderShaderWeight);
        _camcorderScanlineMat.SetFloat(_camcorderShaderWeight, Mathf.Lerp(value, _targetScanlineIntensity, _blendSpeed * Time.deltaTime));
        if (value < 0.001f && _targetScanlineIntensity == 0f)
        {
            _camcorderScanlineMat.SetFloat(_camcorderShaderWeight, 0f);
        }
    }

    public void SetCamcorderZoomScale(float zoomScale)
    {
        zoomScale = Mathf.Clamp01(zoomScale);

        _camcorderLensDistortion.scale.value = 1 + ((_camcorderLensDistortion.scale.max - 1f) * zoomScale);
    }

    public void TriggerCamcorderFlash()
    {
        foreach (var i in _flashCoroutines)
        {
            StopCoroutine(i);
        }
        _flashCoroutines.Clear();
        _flashCoroutines.Add(StartCoroutine(FlashEffectCo()));
    }

    private IEnumerator FlashEffectCo()
    {
        float time = 0f;
        _flashColor.colorFilter.value = Color.white;
        _flashColor.postExposure.value = 5f;
        while (time < _flashEffectLength)
        {
            _camcorderFlashVolume.weight = Mathf.Lerp(0, 1, time / _flashEffectLength);
            time += Time.deltaTime;
            yield return null;
        }

        time = 0f;
        _flashColor.colorFilter.value = Color.black;
        _flashColor.postExposure.value = 0f;
        while (time < _flashFadeLength)
        {
            _camcorderFlashVolume.weight = Mathf.Lerp(1, 0, time / _flashFadeLength);
            time += Time.deltaTime;
            yield return null;
        }
        _camcorderFlashVolume.weight = 0f;
    }
}