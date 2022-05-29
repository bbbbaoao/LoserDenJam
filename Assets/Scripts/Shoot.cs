using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject _helmet;
    [SerializeField]private Rigidbody _helmetrb;
    [SerializeField] private GameObject _skull;
    [SerializeField] private GameObject _skullSeparate;
    [SerializeField] private VisualEffect _explosionEffect;
    [SerializeField] private GameObject _motorCycle;
    [SerializeField]private Rigidbody _motorCyclerb;
    [SerializeField] private GameObject _body;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private Transform _explosionPos;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _upwardMod;
    private int _hitIndex = 0;
    private Animator _animator;
    private Rigidbody _bodyRigidbody;
    [SerializeField] private int _environmentLayer;

    private void Awake()
    {
        _helmetrb.constraints = RigidbodyConstraints.FreezeAll;
        
        _helmetrb.useGravity = false;
        _helmetrb.isKinematic = true;
        _motorCyclerb.constraints = RigidbodyConstraints.FreezeAll;
        _motorCyclerb.useGravity = false;
        _motorCyclerb.isKinematic = true;
        _animator = GetComponent<Animator>();
        _bodyRigidbody = GetComponent<Rigidbody>();
        Physics.IgnoreCollision(_motorCycle.GetComponent<Collider>(), GetComponent<Collider>(), true);
        Physics.IgnoreCollision(_helmet.GetComponent<Collider>(), GetComponent<Collider>(), true);
        //_bodyRigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.layer);
        if(collision.gameObject.layer == _environmentLayer)
        {
            OnObjectHit();

            

        }
    }

    public void OnObjectHit()
    {
        switch(_hitIndex)
        {
            case 0:
                Physics.IgnoreCollision(_helmet.GetComponent<Collider>(), GetComponent<Collider>(), false);
                _helmetrb.constraints = RigidbodyConstraints.None;
                _helmetrb.isKinematic = false;
                _helmet.transform.SetParent(null);
                break;
            case 1:
                _bodyRigidbody.AddExplosionForce(_explosionForce, _explosionPos.position, _explosionRadius, _upwardMod);
                _motorCycle.transform.SetParent(null);
                _animator.SetBool("Flying", true);
                _motorCyclerb.isKinematic=false;
                _motorCyclerb.constraints = RigidbodyConstraints.None;
                Physics.IgnoreCollision(_motorCycle.GetComponent<Collider>(), GetComponent<Collider>(), false);
                break;
            case 2:
                _body.SetActive(false);
                _skull.SetActive(true);
                _bodyRigidbody.AddExplosionForce(_explosionForce, _explosionPos.position, _explosionRadius, _upwardMod);
                _motorCycle.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, _explosionPos.position, _explosionRadius, _upwardMod);
                break;
            case 3:
                _skull.SetActive(false);
                _skullSeparate.SetActive(true);
                _bodyRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                break;
            default:
                return;
        }
        _hitIndex++;
        //_explosionEffect.SendEvent("Explosion");
    }

    public void Reset()
    {
        _helmetrb.constraints = RigidbodyConstraints.FreezeAll;

        _helmetrb.useGravity = false;
        _helmetrb.isKinematic = true;
        _motorCyclerb.constraints = RigidbodyConstraints.FreezeAll;
        _motorCyclerb.useGravity = false;
        _motorCyclerb.isKinematic = true;
        _animator = GetComponent<Animator>();
        _bodyRigidbody = GetComponent<Rigidbody>();
        Physics.IgnoreCollision(_motorCycle.GetComponent<Collider>(), GetComponent<Collider>(), true);
        Physics.IgnoreCollision(_helmet.GetComponent<Collider>(), GetComponent<Collider>(), true);
    }
}
