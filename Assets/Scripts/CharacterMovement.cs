using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _shootingForce = 10f;
    [SerializeField] private float _turnSpeed;
    [SerializeField] private PlayerStats _playerStats;
    private bool _isShot = false;
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
        _isShot = true;
        _rb.constraints = RigidbodyConstraints.None;
        _rb.AddForce(transform.forward * _shootingForce * Mathf.Sqrt(_playerStats.Strength), ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        if (!_isShot)
        //_rb.angularVelocity = (_moveInput.x * transform.up + _moveInput.y * transform.right) * _turnSpeed;
        {
            Vector3 right = transform.right;
            //right.y = 0;
            right.Normalize();
            transform.rotation *= Quaternion.Euler((_moveInput.x * Vector3.up + _moveInput.y * right) * _turnSpeed * Time.deltaTime);
        }

        else
        {
        }
    }
}

