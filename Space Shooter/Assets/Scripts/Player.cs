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
    [SerializeField]
    private List<GameObject> _engines;
    [SerializeField]
    private AudioSource _audioSourceLaser;
    [SerializeField]
    private AudioSource _audioSourceLPowerUp;
    [SerializeField]
    private AudioClip _laserClip;
    [SerializeField]
    private AudioClip _powerUpClip;

    private int _score;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "EnemyLaser")
        {
            Damage();
            Destroy(other.gameObject);
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
        _canFire = Time.time + 1 / _fireRate;
        if (_tripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, new Vector3(transform.position.x, transform.position.y + 0.8f), Quaternion.identity);
        }
        _audioSourceLaser.clip = _laserClip;
        _audioSourceLaser.Play();
    }

    public void Damage()
    {
        if (!_shieldActive)
        {
            _lives--;
            if (_lives > 0)
            {
                UIManager.instance.UpdateLivesDisplay(_lives);

                System.Random rand = new System.Random();
                int engineIndex = rand.Next(0, _engines.Count);
                GameObject engine = _engines[engineIndex];
                engine.SetActive(true);
                _engines.Remove(engine);
            }
            else
            {
                SpawnManager.instance.OnPlayerDeath();
                UIManager.instance.GameOver();
                Destroy(gameObject);
            }
        }
        else
        {
            _shieldActive = false;
            _spawnedShield.GetComponent<Animator>().Play("ShieldDestroy_anim");
            Destroy(_spawnedShield, 0.5f);
            return;
        }
    }

    public void CollectTripleShot()
    {
        _tripleShotActive = true;
        _audioSourceLPowerUp.Play();
        StartCoroutine(TripleShotOff());
    }

    IEnumerator TripleShotOff()
    {
        yield return new WaitForSeconds(powerUpOnTime);
        _tripleShotActive = false;
    }

    public void CollectSpeed()
    {
        _audioSourceLPowerUp.Play();
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
            _audioSourceLPowerUp.Play();
            _spawnedShield = Instantiate(_shieldPrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            _spawnedShield.transform.localScale = new Vector3(2, 2);
            _shieldActive = true; 
        }
    }

    public void SetScore(int addedScore)
    {
        _score += addedScore;
        UIManager.instance.UpdateScoreText();
    }

    public int GetScore()
    {
        return _score;
    }
}
