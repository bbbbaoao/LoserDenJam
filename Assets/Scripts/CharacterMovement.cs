using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _shootingForce = 10f;
    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void ShootPlayer()
    {
        _rb.AddForce(transform.forward * _shootingForce, ForceMode.Impulse);
    }
}
