using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthController : MonoBehaviour {
    private TextMeshProUGUI _text;
    // Start is called before the first frame update
    void Start() {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = GameManager.Instance.player.GetHealth().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = GameManager.Instance.player.GetHealth().ToString();
    }
}
