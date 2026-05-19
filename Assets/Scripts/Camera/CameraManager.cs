using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _sensitivity;
    public float Yaw { get; private set; }
    private float _pitch;
    [SerializeField][Range(0, 90)] private float _pitchLimit;
    [SerializeField] private float _rotationSpeed;
    private Transform _target;
    [SerializeField] private Vector3 _targetOffset;
    private int _targetLayerMask;
    [SerializeField] private float _followSpeed;
    private float _maxRaycastDistance = 200f;

    private void Awake()
    {
    }
    private void Start()
    {
        transform.SetParent(null);
    }

    private void Update()
    {
        
    }

    private void LateUpdate()
    {
        UpdateRotation();
        FollowTransform();
    }

    private void FixedUpdate()
    {
        
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _targetLayerMask = ~(1 << _target.gameObject.layer);
        Yaw = target.rotation.eulerAngles.y;
    }

    public void UpdateInput(float inputX, float inputY)
    {
        Yaw += inputX * _sensitivity;
        _pitch += inputY * _sensitivity;
        _pitch = Mathf.Clamp(_pitch, -_pitchLimit, _pitchLimit);
    }

    public RaycastHit SendRaycast()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit, _maxRaycastDistance, _targetLayerMask, QueryTriggerInteraction.Ignore);
        return hit;
    }

    private void UpdateRotation()
    {
        Quaternion currentRot = transform.rotation;
        Quaternion targetRot = Quaternion.Euler(_pitch, Yaw, 0f);
        float t = _rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(currentRot, targetRot, t);
    }

    private void FollowTransform()
    {
        Vector3 currentPos = transform.position;
        Vector3 targetPos = _target.position;
        targetPos += _target.TransformDirection(_targetOffset);

        float t = _followSpeed * Time.deltaTime;
        transform.position = Vector3.Lerp(currentPos, targetPos, t);
    }
}