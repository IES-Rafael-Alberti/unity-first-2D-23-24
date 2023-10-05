using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpPrecision;
    
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if(Mathf.Abs(_rigidbody.velocity.y) <= jumpPrecision) _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        if (Input.GetAxis("Horizontal") == .0f)
        {
            _animator.SetBool("isRunning", false);
            return;
        }
        _animator.SetBool("isRunning", true);
        var actualPosition = transform.position;
        actualPosition.x += Input.GetAxis("Horizontal") * speed;
        transform.position = actualPosition;
        // set sprite direction
        _spriteRenderer.flipX = Input.GetAxis("Horizontal") < .0f;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(0);
    }

    private void OnCollisionEnter(Collision other)
    {
       SceneManager.LoadScene(0);
    }
}
