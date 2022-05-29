using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterMovement _movement;
    private Vector2 _moveInput;
    private bool _isShot;
    private Shoot _shoot;
    private void Awake()
    {
        _movement = GetComponent<CharacterMovement>();
        Physics.IgnoreLayerCollision(0, 3, true);
        _shoot = GetComponent<Shoot>();
    }
    public void OnMove(InputValue value)
    {
        _moveInput = value.Get<Vector2>();
    }

    public void OnFire(InputValue value)
    {
        if (_isShot) return;
        _movement.ShootPlayer();
        Physics.IgnoreLayerCollision(0, 3, false);
        _shoot.OnObjectHit();
        _isShot = true;

    }

    private void Update()
    {
        _movement.SetMoveInput(_moveInput);
    }
}
