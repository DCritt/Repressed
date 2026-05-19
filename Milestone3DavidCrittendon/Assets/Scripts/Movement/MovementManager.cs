using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private float _baseMoveSpeed;
    private Dictionary<string, float> speedMultipliers = new();
    [SerializeField] private float _baseAcceleration;
    private Dictionary<string, float> accelerationMultipliers = new();
    private bool _moveEnabled = true;
    private Vector2 _moveInput = Vector2.zero;
    [SerializeField] private float _jumpHeight;
    public bool IsGrounded { get; private set; }
    [SerializeField] private float _jumpFloatNullTime;
    private bool _isFloatNull = false;
    private bool _FloatEnabled = true;
    [SerializeField] private float _floatHeight;
    [SerializeField] private float _floatStrength;
    [SerializeField] private float _floatDamping;
    private float _floatRayLength;
    private Rigidbody _rb;
    private Rigidbody _parentRb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _floatRayLength = _floatHeight * 2;
    }

    private void FixedUpdate()
    {
        UpdateGround();
        Float();
        Move();
    }

    private void UpdateGround()
    {
        RaycastHit hit;
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, _floatHeight + (_floatHeight * 0.3f));

        if (IsGrounded)
        {
            _parentRb = hit.rigidbody;
        }
    }

    public void ApplySpeedMult(string name, float value)
    {
        speedMultipliers[name] = value;
    }

    public void RemoveSpeedMult(string name)
    {
        speedMultipliers.Remove(name);
    }

    private float GetSpeedMult()
    {
        float result = 1f;
        foreach (var mult in speedMultipliers.Values)
        {
            result *= mult;
        }

        return result;
    }

    public void ApplyAccMult(string name, float value)
    {
        accelerationMultipliers[name] = value;
    }

    public void RemoveAccMult(string name)
    {
        accelerationMultipliers.Remove(name);
    }

    private float GetAccMult()
    {
        float result = 1f;
        foreach (var acc in accelerationMultipliers.Values)
        {
            result *= acc;
        }

        return result;
    }

    private void Move()
    {
        if (!_moveEnabled) { return; }

        Vector3 direction = transform.TransformDirection(new Vector3(_moveInput.x, 0f, _moveInput.y));

        if (direction.magnitude > 0f || IsGrounded)
        {
            float targetSpeed = _baseMoveSpeed * GetSpeedMult();
            Vector3 targetVelocity = direction * targetSpeed;
            if (_parentRb != null)
            {
                targetVelocity += _parentRb.velocity;
            }

            Vector3 currentVelocity = _rb.velocity;
            currentVelocity.y = 0f;

            float speedInDirection = Vector3.Dot(currentVelocity, direction);
            if (speedInDirection > targetSpeed && !IsGrounded)
            {
                targetVelocity = direction * speedInDirection;
            }

            Vector3 velocityDelta = targetVelocity - currentVelocity;
            float maxAcceleration = _baseAcceleration * GetAccMult();
            Vector3 requiredAcceleration = Vector3.ClampMagnitude(velocityDelta / Time.fixedDeltaTime, maxAcceleration);

            _rb.AddForce(requiredAcceleration, ForceMode.Acceleration);
        }
    }

    public void SetMoveInput(Vector2 moveInput)
    {
        _moveInput = moveInput;
    }

    public void SetMoveEnabled(bool enabled)
    {
        _moveEnabled = enabled;
    }

    public void Rotate(float yaw)
    {
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }

    public void Jump()
    {
        StopAllCoroutines();
        StartCoroutine(JumpCo());

        float jumpForce = Mathf.Sqrt(2f * -Physics.gravity.y * _jumpHeight);
        jumpForce = jumpForce - _rb.velocity.y;
        _rb.AddForce(jumpForce * _rb.mass * Vector3.up, ForceMode.Impulse);
    }

    private IEnumerator JumpCo()
    {
        float time = 0f;
        _isFloatNull = true;

        while (time < _jumpFloatNullTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        _isFloatNull = false;
    }

    public void SetRotation(float yaw)
    {
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }

    public void Float()
    {
        if (_isFloatNull || !_FloatEnabled) { return; }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, _floatRayLength))
        {
            float distance = hit.distance;
            float displacement = _floatHeight - distance;

            float downwardVelocity = Vector3.Dot(_rb.velocity, Vector3.down);

            float floatForce = (displacement * _floatStrength) + (downwardVelocity * _floatDamping) + -Physics.gravity.y;

            _rb.AddForce(Vector3.up * floatForce, ForceMode.Acceleration);
        }
    }

    public void SetFloatEnabled(bool enabled)
    {
        _FloatEnabled = enabled;
    }
}
