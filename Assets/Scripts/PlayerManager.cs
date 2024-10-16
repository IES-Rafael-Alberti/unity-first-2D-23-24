using UnityEditor.Search;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Vector2 bulletForce;
    [SerializeField] private float bulletOffset;
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
        // Jump only if vertical velocity absolute value is less than jumpPrecision 
        if (Input.GetButtonDown("Jump"))
        {
            if(Mathf.Abs(_rigidbody.velocity.y) <= jumpPrecision) _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Shoot();
        }

        // If no horizontal movement stop running animation
        if (Input.GetAxis("Horizontal") == .0f)
        {
            _animator.SetBool("isRunning", false);
            return;
        }
        
        // Activate running animation
        _animator.SetBool("isRunning", true);
        // Calculate new position
        var actualPosition = transform.position;
        actualPosition.x += Input.GetAxis("Horizontal") * speed;
        transform.position = actualPosition;
        // set sprite direction
        _spriteRenderer.flipX = Input.GetAxis("Horizontal") < .0f;
    }

    void Shoot()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position + Vector3.up * bulletOffset, Quaternion.identity);
        Vector2 directedBulletForce = new Vector2(bulletForce.x * (_spriteRenderer.flipX? -1:1), bulletForce.y); 
        newBullet.GetComponent<Rigidbody2D>().AddForce(directedBulletForce, ForceMode2D.Impulse);
    }
}
