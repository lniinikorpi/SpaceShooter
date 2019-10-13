using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4;
    public int health = 3;

    private float _yBoundTop = 7;
    private float _yBoundBottom = -5.5f;
    private float _xBound = 9.5f;
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y <= _yBoundBottom)
        {
            float randX = Random.Range(-_xBound, _xBound);
            transform.position = new Vector3(randX, _yBoundTop);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            Destroy(gameObject);
        }
        else if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            health--;
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
