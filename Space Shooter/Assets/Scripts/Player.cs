using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    private float _speedMultiplier = 2;
    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private GameObject _laserPrefab;

    private float _xBounds = 11.3f;
    private float _yBoundsTop = 0;
    private float _yBoundsBottom = -3.75f;

    [SerializeField]
    private float _fireRate = 5f;
    private float _canFire = -1;

    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldPrefab;
    private GameObject _spawnedShield;
    private bool _tripleShotActive = false;
    private bool _shieldActive = false;
    [SerializeField]
    private float powerUpOnTime = 3;
    void Start()
    {
        transform.position = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            CalculateMovement(); 
        }
        if (Input.GetButton("Fire1") && Time.time > _canFire)
        {
            Shoot();
        }
    }

    void CalculateMovement()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(movement * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _yBoundsBottom, _yBoundsTop));

        if (transform.position.x <= -_xBounds)
        {
            transform.position = new Vector3(_xBounds, transform.position.y);
        }
        else if (transform.position.x >= _xBounds)
        {
            transform.position = new Vector3(-_xBounds, transform.position.y);
        }

    }

    void Shoot()
    {
        _canFire = Time.time + 1/_fireRate;
        if(_tripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 0.8f), Quaternion.identity); 
        }
    }

    public void Damage()
    {
        if (!_shieldActive)
        {
            _lives--; 
        }
        else
        {
            _shieldActive = false;
            _spawnedShield.GetComponent<Animator>().Play("ShieldDestroy_anim");
            Destroy(_spawnedShield, 0.5f);
            return;
        }
        if(_lives <= 0)
        {
            SpawnManager.instance.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    public void CollectTripleShot()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotOff());
    }

    IEnumerator TripleShotOff()
    {
        yield return new WaitForSeconds(powerUpOnTime);
        _tripleShotActive = false;
    }

    public void CollectSpeed()
    {
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedOff());
    }

    private IEnumerator SpeedOff()
    {
        yield return new WaitForSeconds(powerUpOnTime);
        _speed /= _speedMultiplier;
    }

    public void CollectShield()
    {
        if (!_shieldActive)
        {
            _spawnedShield = Instantiate(_shieldPrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            _spawnedShield.transform.localScale = new Vector3(2, 2);
            _shieldActive = true; 
        }
    }
}
