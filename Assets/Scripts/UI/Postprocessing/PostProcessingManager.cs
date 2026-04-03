using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager Instance { get; private set; }

    [field: SerializeField] public Volume CamcorderVolume { get; private set; }
    [field: SerializeField] public Volume CamcorderFlashVolume { get; private set; }
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
        CamcorderFlashVolume.profile.TryGet<ColorAdjustments>(out _flashColor);
        CamcorderFlashVolume.profile.TryGet<Bloom>(out _flashBloom);
    }

    private void Update()
    {
        CamcorderVolume.weight = Mathf.Lerp(CamcorderVolume.weight, _camcorderTargetWeight, _blendSpeed * Time.deltaTime);
    }

    public void SetCamcorderEnabled(bool enabled)
    {
        _camcorderTargetWeight = enabled ? 1f : 0f;
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
            CamcorderFlashVolume.weight = Mathf.Lerp(0, 1, time / _flashEffectLength);
            time += Time.deltaTime;
            yield return null;
        }

        time = 0f;
        _flashColor.colorFilter.value = Color.black;
        _flashColor.postExposure.value = 0f;
        while (time < _flashFadeLength)
        {
            CamcorderFlashVolume.weight = Mathf.Lerp(1, 0, time / _flashFadeLength);
            time += Time.deltaTime;
            yield return null;
        }
        CamcorderFlashVolume.weight = 0f;
    }
}