using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    private void Awake() {
        Instance = this;
        player = playerObject.GetComponent<PlayerController>();
    }

    public GameObject popUpText;
    public GameObject playerObject;
    public PlayerController player { get; private set; } 

    public void ShowPopup(Vector3 position, string text = "5") {
        GameObject newPopup = Instantiate(popUpText, position, Quaternion.identity);
        newPopup.GetComponent<PopupController>().text = text;
    }
}
