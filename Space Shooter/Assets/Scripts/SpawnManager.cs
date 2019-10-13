using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemies;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private bool _spawnEnemy;
    private bool _spawnTripleShot;
    [SerializeField]
    private float _waitTime = 2;
    [SerializeField]
    private float _powerUpMaxWait = 7;
    [SerializeField]
    private float _powerUpMinWait = 3;
    private float _xBound = 9.5f;
    private float _yBound = 7.5f;
    private bool _stopSpawing = false;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_spawnEnemy == false)
        {
            StartCoroutine(SpawnEnemy());
        }
        if (_spawnTripleShot == false)
        {
            StartCoroutine(SpawnPowerUp());
        }
    }

    private IEnumerator SpawnEnemy()
    {
        if (_stopSpawing == false)
        {
            _spawnEnemy = true;
            float randX = Random.Range(-_xBound, _xBound);
            Instantiate(_enemyPrefab, new Vector3(randX, _yBound, 0), Quaternion.identity, _enemies.transform);
            yield return new WaitForSeconds(_waitTime);
            _spawnEnemy = false; 
        }
    }
    private IEnumerator SpawnPowerUp()
    {
        if (_stopSpawing == false)
        {
            _spawnTripleShot = true;
            float randX = Random.Range(-_xBound, _xBound);
            Instantiate(_tripleShotPrefab, new Vector3(randX, _yBound, 0), Quaternion.identity);
            float wait = Random.Range(_powerUpMinWait, _powerUpMaxWait);
            yield return new WaitForSeconds(wait);
            _spawnTripleShot = false;
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawing = true;
    }
}
