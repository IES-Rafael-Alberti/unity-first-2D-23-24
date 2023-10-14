using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    
    private Rigidbody2D _rigidbody;
    private float _initialPos;
    private Vector2 _direction;

    // Start is called before the first frame update
    void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
        _initialPos = transform.position.x;
        _direction = Vector2.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x - _initialPos < .0f) _direction = Vector2.right * speed;    
        if(transform.position.x - _initialPos > distance) _direction = Vector2.left * speed;
        _rigidbody.position += _direction;
    }
}
