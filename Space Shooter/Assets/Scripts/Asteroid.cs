using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    public int health = 2;
    [SerializeField]
    private float _rotationSpeedMax = 50;
    private float _rotationSpeed;
    [SerializeField]
    private int _scoreGiven = 10;

    private float _yBoundTop = 7;
    private float _yBoundBottom = -5.5f;
    private float _xBound = 9.5f;

    private Player _player;
    private bool _isDestroyed = false;

    private Animator _anim;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        if (_player == null)
        {
            Debug.LogError("Player is NULL!");
        }
        if (_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }
        _rotationSpeed = Random.Range(-_rotationSpeedMax, _rotationSpeedMax);
    }
    void Update()
    {
        transform.position += (Vector3.down * _speed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, _rotationSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            _player.Damage();
            DestroyAsteroid();
        }
        else if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            health--;
            if (health <= 0)
            {
                _player.SetScore(_scoreGiven);
                DestroyAsteroid();
            }
        }
    }

    void DestroyAsteroid()
    {
        _isDestroyed = true;
        SpawnManager.instance.DestroyedAsteroids++;
        _anim.SetTrigger("isDestroyed");
        GetComponent<CircleCollider2D>().enabled = false;
        Destroy(gameObject, 2.5f);
    }
}
