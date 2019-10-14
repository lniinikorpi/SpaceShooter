using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4;
    public int health = 3;
    [SerializeField]
    private int _scoreGiven = 10;

    private float _yBoundTop = 7;
    private float _yBoundBottom = -5.5f;
    private float _xBound = 9.5f;

    private Player _player;
    private bool _isDead = false;

    private Animator _anim;
    private AudioSource _audioSource;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        if(_player == null)
        {
           Debug.LogError("Player is NULL!");
        }
        if(_anim == null)
        {
            Debug.LogError("Animator is NULL");
        }
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("Audio source is NULL");
        }
    }
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= _yBoundBottom && !_isDead)
        {
            float randX = Random.Range(-_xBound, _xBound);
            transform.position = new Vector3(randX, _yBoundTop);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            _player.Damage();
            DestroyEnemy();
        }
        else if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            health--;
            if(health <= 0)
            {
                _player.SetScore(_scoreGiven);
                DestroyEnemy();
            }
        }
    }

    void DestroyEnemy ()
    {
        _isDead = true;
        _anim.SetTrigger("isDead");
        _audioSource.Play();
        GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject, 2.5f);
    }
}
