using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour {
    // destiny scene index
    public int index;
    private void OnTriggerEnter2D(Collider2D other) {
        // activation with player collision
        if(other.gameObject.CompareTag("Player")) SceneManager.LoadScene(index);
    }
}
