using System.Collections;
using System.Threading.Tasks;
using UnityEditor.Search;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Vector2 bulletForce;
    [SerializeField] private float bulletOffset;
    [SerializeField] private float bulletCooldown;
    
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpPrecision;
    
    [SerializeField] private GameObject gun;
    [SerializeField] private LineRenderer laser;
    [SerializeField] private float laserSize;
    [SerializeField] private float laserDuration;
    [SerializeField] private float laserCooldown;
    
    [SerializeField] private GameObject shatterEffect;
    
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(Shoot1CD());
        StartCoroutine(Shoot2CD());
    }

    // Update is called once per frame
    void Update()
    {
        // Jump only if vertical velocity absolute value is less than jumpPrecision 
        if (Input.GetButtonDown("Jump"))
        {
            if(Mathf.Abs(_rigidbody.velocity.y) <= jumpPrecision) _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        if (GameManager.Instance.player.CanShoot(0) && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Shoot1());
            StartCoroutine(Shoot1CD());
        }
        if (GameManager.Instance.player.CanShoot(1) && Input.GetButtonDown("Fire2"))
        {
           Shoot2();
           StartCoroutine(Shoot2CD());
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

    
    IEnumerator Shoot1() {
        Vector3 actualShootOffset = gun.transform.position - transform.position;
        actualShootOffset.x *= _spriteRenderer.flipX ? 1 : -1;
        Vector3 shootDirection = _spriteRenderer.flipX ? Vector3.left : Vector3.right;
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position + actualShootOffset, shootDirection);
        laser.SetPosition(0, transform.position + actualShootOffset );
        laser.SetPosition(1, transform.position + actualShootOffset + shootDirection*laserSize);
        
        if (hitInfo) {
            laser.SetPosition(1, hitInfo.point);
            Instantiate(shatterEffect, hitInfo.point, Quaternion.identity);
            GameManager.Instance.ShowPopup(hitInfo.point, 3);
        }
        
        laser.enabled = true;
        yield return new WaitForSeconds(laserDuration);
        laser.enabled = false;
    }

    IEnumerator Shoot1CD() {
        GameManager.Instance.player.SetShoot(0, false);
        yield return new WaitForSeconds(laserCooldown);
        GameManager.Instance.player.SetShoot(0, true);
    }
    
    void Shoot2()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position + Vector3.up * bulletOffset, Quaternion.identity);
        Vector2 directedBulletForce = new Vector2(bulletForce.x * (_spriteRenderer.flipX? -1:1), bulletForce.y); 
        newBullet.GetComponent<Rigidbody2D>().AddForce(directedBulletForce, ForceMode2D.Impulse);
    }
    IEnumerator Shoot2CD() {
        GameManager.Instance.player.SetShoot(1, false);
        yield return new WaitForSeconds(bulletCooldown);
        GameManager.Instance.player.SetShoot(1, true);
    }
    
}
