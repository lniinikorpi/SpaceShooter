using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType {
    Tripleshot,
    Speed,
    Shield
};

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private PowerupType _powerupType;
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
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null) {
                switch (_powerupType)
                {
                    case PowerupType.Tripleshot:
                        player.CollectTripleShot();
                        break;
                    case PowerupType.Speed:
                        player.CollectSpeed();
                        break;
                    case PowerupType.Shield:
                        player.CollectShield();
                        break;
                    default:
                        break;
                }
            }
            Destroy(gameObject);
        }
    }
}
