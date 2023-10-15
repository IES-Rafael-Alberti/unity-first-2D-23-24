using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    [SerializeField] private List<GameObject> pathObjects;
    [SerializeField] private float speed;

    private List<Vector3> _pathList;
    private int _numPoints;
    private int _actualPoint;
    // Start is called before the first frame update
    void Start() {
        pathObjects.Insert(0, gameObject);
        _numPoints = pathObjects.Count;
        if (_numPoints == 0) enabled = false;
        _pathList = pathObjects.Select(x => x.transform.position).ToList();
        _actualPoint = 0;
    }

    // Update is called once per frame
    void Update() {
        if (Vector2.Distance(transform.position, _pathList[_actualPoint%_numPoints]) < 0.1f) _actualPoint++;
        transform.position = Vector3.MoveTowards(transform.position, _pathList[_actualPoint % _numPoints],
            speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) other.gameObject.transform.parent = transform;
    }
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) other.gameObject.transform.parent = null;
    }
}
