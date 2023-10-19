using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    [SerializeField] private GameObject shatterEffect;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Disapear());
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Door")) return;
        GameManager.Instance.ShowPopup(transform.position, 15);
        DestroyThis();
    }
    IEnumerator Disapear()
    {
        yield return new WaitForSeconds(5);
        DestroyThis();
    }

    void DestroyThis() {
        Instantiate(shatterEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
