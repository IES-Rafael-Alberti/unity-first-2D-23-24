using System;
using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    private void Awake() {
        if (Instance != null) {
            Destroy(this);
            return;
        }
        Instance = this;
        player.Init();
        DontDestroyOnLoad(gameObject);
    }

    public GameObject popUpText;
    public Player player; 

    public void ShowPopup(Vector3 position, int number, string text = "5") {
        player.ChangeHealth(-number);
        GameObject newPopup = Instantiate(popUpText, position, Quaternion.identity);
        newPopup.GetComponent<PopupController>().text = number.ToString();
    }
}
