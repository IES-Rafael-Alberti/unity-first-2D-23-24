using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformController : MonoBehaviour {
    private const float OFFSET = 0.1f;
    
    [SerializeField] private List<GameObject> pathObjects;
    [SerializeField] private float speed;

    private List<Vector3> _pathList;
    private int _numPoints;
    private int _actualPoint;
    
    #region Unity standard events
    // Start is called before the first frame update
    private void Start() {
        // insert initial object position in path list 
        pathObjects.Insert(0, gameObject);
        _numPoints = pathObjects.Count;
        // if empty path list disable platform 
        if (_numPoints == 0) enabled = false;
        // init path list
        initPathList();
        // set first position
        _actualPoint = 0;
    }

    // Update is called once per frame
    private void Update() {
        // checks distance to next path point
        if (Vector2.Distance(transform.position, _pathList[_actualPoint%_numPoints]) < OFFSET) _actualPoint++;
        // move towards next point
        // HACK: to cycle list we use _actualPoint % _numPoints as index
        transform.position = Vector3.MoveTowards(transform.position, _pathList[_actualPoint % _numPoints],
            speed * Time.deltaTime);
    }
    #endregion

    #region Player collision detection
    private void OnCollisionEnter2D(Collision2D other) {
        // TODO: Detect collisions only in platform surface
        if (other.gameObject.CompareTag("Player")) other.gameObject.transform.parent = transform;
    }
    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) other.gameObject.transform.parent = null;
    }
    #endregion

    #region List initialization
    private void initPathList() {
        // with loops
        // _pathList = new List<Vector3>();
        // foreach (var pathObject in pathObjects)
        // {
        //     _pathList.Add(pathObject.transform.position);
        // }
        // without loops but with lambda
        _pathList = pathObjects.Select(x => x.transform.position).ToList();
        // without loops but with named method
        //_pathList = pathObjects.Select(ExtractPosition).ToList();
    }

    // named method for list initialization
    private Vector3 ExtractPosition(GameObject x)
    {
        return x.transform.position;
    }
    #endregion

}
