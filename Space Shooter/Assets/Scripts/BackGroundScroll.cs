using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    private float _yBoundBottom = -10.88f;
    private float _yBoundTop = 12.91f;
    [SerializeField]
    private float _scrollSpeed = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _scrollSpeed * Time.deltaTime);
        if(transform.position.y < _yBoundBottom)
        {
            transform.position = new Vector3(transform.position.x, _yBoundTop);
        }
    }
}
