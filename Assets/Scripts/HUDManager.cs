using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {
    [SerializeField] private int weaponIndex;
    [SerializeField] private Color enabledColor;
    [SerializeField] private Color disabledColor;

    private Image _image;
    // Start is called before the first frame update
    void Start() {
        _image = GetComponent<Image>();
        _image.color = Color.green;
    }

    // Update is called once per frame
    void Update() {
        _image.color = GameManager.Instance.player.CanShoot(weaponIndex) ? enabledColor : disabledColor; 
    }
}
