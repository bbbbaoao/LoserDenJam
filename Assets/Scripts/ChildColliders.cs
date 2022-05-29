using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildColliders : MonoBehaviour
{
    private Shoot _shoot;
        private void Awake()
    {
        _shoot = GetComponentInParent<Shoot>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        _shoot.OnObjectHit();
    }
}
