using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Disapear());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DestroyThis();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DestroyThis();
    }
    IEnumerator Disapear()
    {
        yield return new WaitForSeconds(5);
        DestroyThis();
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
