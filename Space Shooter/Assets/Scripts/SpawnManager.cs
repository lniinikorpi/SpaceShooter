﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _asteroidPrefab;
    [SerializeField]
    private GameObject _enemies;
    [SerializeField]
    private List<GameObject> _powerUps = new List<GameObject>();
    private bool _spawnEnemy;
    private bool _spawnPowerUp;
    [SerializeField]
    private float _waitTime = 2;
    [SerializeField]
    private float _powerUpMaxWait = 7;
    [SerializeField]
    private float _powerUpMinWait = 3;
    private float _xBound = 9.5f;
    private float _yBound = 7.5f;
    private bool _stopSpawing = false;
    [SerializeField]
    private int _numberOfAsteroidsToShoot = 10;

    [HideInInspector]
    public int DestroyedAsteroids;
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
        if (_spawnPowerUp == false && DestroyedAsteroids >= _numberOfAsteroidsToShoot)
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
            GameObject enemyToSpawn;
            if(DestroyedAsteroids < _numberOfAsteroidsToShoot)
            {
                enemyToSpawn = _asteroidPrefab;
            }
            else
            {
                enemyToSpawn = _enemyPrefab;
            }
            Instantiate(enemyToSpawn, new Vector3(randX, _yBound, 0), Quaternion.identity, _enemies.transform);
            yield return new WaitForSeconds(_waitTime);
            _spawnEnemy = false; 
        }
    }
    private IEnumerator SpawnPowerUp()
    {
        if (_stopSpawing == false)
        {
            _spawnPowerUp = true;
            float randX = Random.Range(-_xBound, _xBound);
            int powerUp = Random.Range(0, _powerUps.Count);
            Instantiate(_powerUps[powerUp], new Vector3(randX, _yBound, 0), Quaternion.identity);
            float wait = Random.Range(_powerUpMinWait, _powerUpMaxWait);
            yield return new WaitForSeconds(wait);
            _spawnPowerUp = false;
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawing = true;
    }
}
