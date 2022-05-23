using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _shootingForce = 10f;
    [SerializeField]private float _turnSpeed;
    private Rigidbody _rb;
    private Vector2 _moveInput;

    public void SetMoveInput(Vector2 input)
    {
        _moveInput = input;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezePosition;
    }

    public void ShootPlayer()
    {
        _rb.AddForce(transform.forward * _shootingForce, ForceMode.Impulse);
        _rb.constraints = RigidbodyConstraints.None;
    }

    private void FixedUpdate()
    {
        //Vector3 LookDirection = new Vector3(_moveInput.x, 0f, _moveInput.y).normalized;
        //float horturnDirection = -Mathf.Sign(_moveInput.x);
        //float turnMagnitude1 = 1f - Mathf.Clamp01(Vector3.Dot(transform.forward, LookDirection));
        //float vertTemp = Vector3.Dot(Vector3.Cross(LookDirection, transform.forward), transform.right);
        //float verturnDirection = -Mathf.Sign(vertTemp);
        //float turnMagnitude2 = 1f - Mathf.Clamp01(vertTemp);
        //if (_moveInput.magnitude < 0.1f) { turnMagnitude1 = 0f;
        //    turnMagnitude2 = 0f;
        //}
        //float angularVelocity1 = turnMagnitude1 * _turnSpeed * horturnDirection;
        //Vector3 angularVelocity2 = transform.right * turnMagnitude2 * _turnSpeed * verturnDirection;
        //angularVelocity2.y += angularVelocity1;
        _rb.angularVelocity = (_moveInput.x * transform.up + _moveInput.y * transform.right) * _turnSpeed;
        //transform.rotation *= Quaternion.LookRotation((_moveInput.x * transform.up + _moveInput.y * transform.right) * _turnSpeed);
    }
}
