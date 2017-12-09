using UnityEngine;
using System.Collections;
using System;

public class PrimaryWeaponManager : MonoBehaviour, IWeapon {

    public GameObject _bulletPrefab;
    public Transform _shotPoint;

    [Header("Shooting-Behaviour:\n")]

    [Header("Number of Bullets per Shot\n")]
    public int _bulletsAmount = 1;

    [Header("Properties of Shots\n")]

    public bool _singleShot = true; 

    public float _shotDelay = 0.3f;
    public float _spreadAmount = 0.1f;
    public float _offsetAmount = 0.45f;
    public float _accuracy = 1f;

    [Header("Critical Hit\n")]
    public float _criticalHitChance = 0.1f;
    public float _criticalHitFactor = 2.0f;

    public AudioSource ShootingSound;

    /*public int _shotDelay;
    public int _spreadAmount;
    public int _offsetAmount;
    public int _accuracy;
    public int _criticalHitChance;
    public int _criticalHitAmount;*/
    //TODO: critical hit chance, critical hit amount, base attack, modifier, passive skills etc...

    private float _spreadAmountF;
    private float _offsetAmountF;
    private float _accuracyF;
    private float _criticalHitChanceF;
    private float _criticalHitAmountF;

    [Header("Type of Bullets\n")]
    //TODO


    private bool _shooting = false;

    private float _lastCall;

    //// Use this for initialization
    void Start () {
        _lastCall = _shotDelay;/*/1000.0f;
        _spreadAmountF = _spreadAmount / 1000.0f;
        _offsetAmountF = _offsetAmount / 1000.0f;
        _accuracyF = _accuracy / 1000.0f;*/

        Debug.Log("Shot Delay: " + _lastCall);
    }

    // Update is called once per frame
    void Update() {

        _lastCall += Time.deltaTime;

        if(_shooting && _lastCall >= _shotDelay)
        {
            Shoot();
            _lastCall = 0f;
        }

    }

    public void deactivate()
    {
        _shooting = false;
    }

    public void activate()
    {
        if(_singleShot)
        {
            Shoot();
        }
        else
        {
            _shooting = true;
        }
        
    }

    public void Shoot()
    {
        if (_bulletsAmount % 2 == 1)
        {
            shootBullet(_shotPoint.position, _shotPoint.rotation);
        }
        for (int i = 1; i <= Mathf.RoundToInt(_bulletsAmount / 2); i++)
        {
            Vector3 offset = Vector3.zero;
            Quaternion spread = Quaternion.identity;

            if (!Mathf.Approximately(_offsetAmount, 0f))
            {
                offset = _shotPoint.right * i * _offsetAmount;
            }

            if (!Mathf.Approximately(_spreadAmount, 0f))
            {
                spread.y -= (_spreadAmount / 10f) * i;
                shootBullet(_shotPoint.position - offset, _shotPoint.rotation * spread);
                spread.y += (_spreadAmount / 10f) * i * 2;
                shootBullet(_shotPoint.position + offset, _shotPoint.rotation * spread);
            }
            else
            {
                shootBullet(_shotPoint.position - offset, _shotPoint.rotation);
                shootBullet(_shotPoint.position + offset, _shotPoint.rotation);
            }
        }
    }

    private void shootBullet(Vector3 pos, Quaternion rot)
    {
        //TODO: Instantiate different Bullet-Prefabs here-------------

        float jitter = UnityEngine.Random.Range(-0.1f, 0.1f) * (1f - _accuracy);
        rot.y += jitter;
        _bulletPrefab.Spawn(pos, rot);
        ShootingSound.Play();
        Debug.Log("PEW");
    }
}
