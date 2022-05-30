using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Events;
using TMPro;

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
    public UnityEvent OnDeath;

    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _strengthText;
    [SerializeField] private PlayerStats _playerStats;
    private int _gainedCoin;

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
                _helmetrb.useGravity = true;
                _helmet.transform.SetParent(null);
                _explosionEffect.SendEvent("Explosion");
                break;
            case 1:
                _bodyRigidbody.AddExplosionForce(_explosionForce, _explosionPos.position, _explosionRadius, _upwardMod);
                _motorCycle.transform.SetParent(null);
                _animator.SetBool("Flying", true);
                _motorCyclerb.isKinematic=false;
                _motorCyclerb.constraints = RigidbodyConstraints.None;
                Physics.IgnoreCollision(_motorCycle.GetComponent<Collider>(), GetComponent<Collider>(), false);        
                _motorCyclerb.useGravity = true;
                _motorCycle.GetComponent<Rigidbody>().AddExplosionForce(_explosionForce, _explosionPos.position, _explosionRadius, _upwardMod);
                _explosionEffect.SendEvent("Explosion");
                break;
            case 2:
                _body.SetActive(false);
                _skull.SetActive(true);
                _bodyRigidbody.AddExplosionForce(_explosionForce, _explosionPos.position, _explosionRadius, _upwardMod);
                _explosionEffect.SendEvent("Explosion");
                break;
            case 3:
                _skull.SetActive(false);
                _skullSeparate.SetActive(true);
                _bodyRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                StartCoroutine(Dead());
                break;
            default:
                return;
        }
        _hitIndex++;

    }

    //public void Reset()
    //{
        
    //    _helmetrb.constraints = RigidbodyConstraints.FreezeAll;
    //    _bodyRigidbody.constraints= RigidbodyConstraints.FreezePosition;
    //    _helmetrb.useGravity = false;
    //    _helmetrb.isKinematic = true;
    //    _motorCyclerb.constraints = RigidbodyConstraints.FreezeAll;
    //    _motorCyclerb.useGravity = false;
    //    _motorCyclerb.isKinematic = true;
    //    Physics.IgnoreCollision(_motorCycle.GetComponent<Collider>(), GetComponent<Collider>(), true);
    //    Physics.IgnoreCollision(_helmet.GetComponent<Collider>(), GetComponent<Collider>(), true);

    //}
    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(5f);
        int score = (int)Vector3.Distance(gameObject.transform.position, Vector3.zero) * 100;
        _scoreText.text = "score: " + score.ToString();
        _gainedCoin = score / 10000 + score / 100000 * 10 + score / 300000 * 30;
        _playerStats.Money += _gainedCoin;
        _coinText.text = "coin: " + _playerStats.Money.ToString() + " (+ " + _gainedCoin.ToString() + ")";
        _strengthText.text = _playerStats.Strength.ToString();
        OnDeath.Invoke();
    }

    public void AddCoin()
    {
        if (_playerStats.Money >= 100)
        {
            _playerStats.Money -= 100;
            _coinText.text = "coin: " + _playerStats.Money.ToString() + " + " + _gainedCoin.ToString();
            _playerStats.Strength += 1;
            _strengthText.text = _playerStats.Strength.ToString();
        }       
    }
}
