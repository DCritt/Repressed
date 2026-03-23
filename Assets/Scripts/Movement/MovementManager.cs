using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField] private float _baseMoveSpeed;
    [SerializeField] private float _baseAcceleration;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpFloatNullTime;
    private bool _isFloatNull = false;
    private bool _FloatEnabled = true;
    [SerializeField] private float _floatHeight;
    [SerializeField] private float _floatStrength;
    [SerializeField] private float _floatDamping;
    private float _floatRayLength;
    public Rigidbody Rb;

    private void Start()
    {
        _floatRayLength = _floatHeight * 2;
    }

    private void FixedUpdate()
    {
        Float();
    }

    public void Move(Vector2 moveInput, float moveMult, float accMult)
    {
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y);

        float targetSpeed = _baseMoveSpeed * moveMult;
        Vector3 targetVelocity = direction * targetSpeed;

        Vector3 currentVelocity = Rb.velocity;
        currentVelocity.y = 0f;

        Vector3 velocityDelta = targetVelocity - currentVelocity;
        float maxAcceleration = _baseAcceleration * accMult;
        Vector3 requiredAcceleration = Vector3.ClampMagnitude(velocityDelta / Time.fixedDeltaTime, maxAcceleration);

        Rb.AddForce(requiredAcceleration, ForceMode.Acceleration);
    }

    public void Jump()
    {
        StopAllCoroutines();
        StartCoroutine(JumpCo());

        float jumpForce = Mathf.Sqrt(2f * -Physics.gravity.y * _jumpHeight);
        jumpForce = jumpForce - Rb.velocity.y;
        Rb.AddForce(jumpForce * Rb.mass * Vector3.up, ForceMode.Impulse);
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

    public void Float()
    {
        if (_isFloatNull || !_FloatEnabled) { return; }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, _floatRayLength))
        {
            float distance = hit.distance;
            float displacement = _floatHeight - distance;

            float downwardVelocity = Vector3.Dot(Rb.velocity, Vector3.down);

            float floatForce = (displacement * _floatStrength) + (downwardVelocity * _floatDamping) + -Physics.gravity.y;

            Rb.AddForce(Vector3.up * floatForce, ForceMode.Acceleration);
        }
    }

    public void SetFloatEnabled(bool enabled)
    {
        _FloatEnabled = enabled;
    }

    public bool IsGrounded() => Physics.Raycast(transform.position, Vector3.down, _floatHeight + (_floatHeight * 0.1f));
}
