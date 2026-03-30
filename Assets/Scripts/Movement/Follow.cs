using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private float _followSpeed;
    private bool _enabled = true;

    private void Awake()
    {
        
    }

    private void Start()
    {
        if (transform.parent != null)
        {
            transform.SetParent(null);
        }
    }

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        if (!_enabled) { return; }

        Vector3 currentPos = transform.position;
        Vector3 targetPos = _target.position;
        float t = _followSpeed * Time.deltaTime;
        transform.position = Vector3.Lerp(currentPos, targetPos, t);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void SetEnabled(bool enabled)
    {
        _enabled = enabled;
    }
}
