using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthController : MonoBehaviour {
    private TextMeshProUGUI _text;
    // Start is called before the first frame update
    private void Start() {
        // get text component
        _text = GetComponent<TextMeshProUGUI>();
        // init text to player health
        _text.text = GameManager.Instance.player.GetHealth().ToString();
    }

    // Update is called once per frame
    private void Update()
    {
        // set text to player health
        _text.text = GameManager.Instance.player.GetHealth().ToString();
    }
}
