using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterMovement _movement;
    private Vector2 _moveInput;
    private void Awake()
    {
        _movement = GetComponent<CharacterMovement>();
    }
    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    public void OnFire(InputValue value)
    {
        _movement.ShootPlayer();
    }

    private void Update()
    {
        _movement.SetMoveInput(_moveInput);
    }
}
