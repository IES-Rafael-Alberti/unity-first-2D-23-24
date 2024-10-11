using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletController : MonoBehaviour {
    #region Parameters declaration
    [SerializeField] private GameObject shatterEffect;
    [SerializeField] private int damage = 15;
    [SerializeField] private int disappearTimeout = 5;
    #endregion

    #region Unity standard events 
    // Start is called before the first frame update
    private void Start()
    {
        // destroy timeout
        StartCoroutine(Disappear());
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // don't destroy on doors
        if(other.gameObject.CompareTag("Door")) return;
        // show damage popup
        GameManager.Instance.ShowPopup(transform.position, damage);
        // destroy bullet
        DestroyThis();
    }
    #endregion

    #region Helper methods
    private IEnumerator Disappear()
    {
        // set timeout
        yield return new WaitForSeconds(disappearTimeout);
        // destroy after timeout
        DestroyThis();
    }

    private void DestroyThis() {
        // instantiate bullet destroy particle effect
        Instantiate(shatterEffect, transform.position, Quaternion.identity);
        // destroy bullet
        Destroy(gameObject);
    }
    #endregion
}
