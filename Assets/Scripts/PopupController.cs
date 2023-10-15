using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupController : MonoBehaviour {
    [SerializeField] private float lifeTime;
    [SerializeField] private float distance;
    [SerializeField] private float speed;
    [SerializeField] private float fade;

    public string text;
    //[SerializeField] private Canvas canvas;

    private Vector3 _destiny;

    private TextMeshPro _text;

    private Color _colorFade;
    // Start is called before the first frame update
    void Start() {
        _destiny = transform.position + Vector3.up * distance;
        //transform.SetParent(canvas.transform);
        transform.localScale = Vector3.one;
        _text = GetComponent<TextMeshPro>();
        _text.text = text;
        _colorFade = _text.color;
        _colorFade.a = 0;
        StartCoroutine(DestroyMe());
    }

    // Update is called once per frame
    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, _destiny, speed*Time.deltaTime);
        _text.color = Color.Lerp(_text.color, _colorFade, fade * Time.deltaTime);
    }

    IEnumerator DestroyMe() {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
