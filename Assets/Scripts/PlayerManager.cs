using System.Collections;
using UnityEditor.Search;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpPrecision;
    [SerializeField] private GameObject gun;
    [SerializeField] private LineRenderer laser;
    
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
        // Jump only if vertical velocity absolute value is less than jumpPrecision 
        if (Input.GetButtonDown("Jump"))
        {
            if(Mathf.Abs(_rigidbody.velocity.y) <= jumpPrecision) _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        
        // Shoot
        if (Input.GetButtonDown("Fire1")) StartCoroutine(Shoot());
        
        // If no horizontal movement stop running animation
        if (Input.GetAxis("Horizontal") == .0f)
        {
            _animator.SetBool("isRunning", false);
            return;
        }
        
        // Activate running animation
        _animator.SetBool("isRunning", true);
        //_rigidbody.velocity = Vector2.right * (Input.GetAxis("Horizontal") * speed);
        // Calculate new position
        var actualPosition = _rigidbody.position;
        actualPosition.x += Input.GetAxis("Horizontal") * speed;
        _rigidbody.position = actualPosition;
        // set sprite direction
        _spriteRenderer.flipX = Input.GetAxis("Horizontal") < .0f;
    }

    IEnumerator Shoot() {
        Vector3 actualShootOffset = gun.transform.position - transform.position;
        actualShootOffset.x *= _spriteRenderer.flipX ? -1 : 1;
        Vector3 shootDirection = _spriteRenderer.flipX ? Vector3.left : Vector3.right;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position + actualShootOffset, shootDirection);
        laser.SetPosition(0, transform.position + actualShootOffset );
        laser.SetPosition(1, transform.position + actualShootOffset + shootDirection*100);
        
        if (hitInfo) {
            laser.SetPosition(1, hitInfo.point);
        }
        
        laser.enabled = true;
        yield return new WaitForSeconds(0.09f);
        laser.enabled = false;
    }
}
