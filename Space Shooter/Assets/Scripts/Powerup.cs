using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3;
    private float _yBoundsBottom = -5.5f;
    private float _yboundsTop = 7;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= _yBoundsBottom)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector2.down * Time.deltaTime * _speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (other.gameObject.GetComponent<Player>() != null) {
                other.gameObject.GetComponent<Player>().CollectTripleShot();
            }
            Destroy(gameObject);
        }
    }
}
