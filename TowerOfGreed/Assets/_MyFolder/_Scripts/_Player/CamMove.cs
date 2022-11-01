using UnityEngine;
using System.Collections;

public class CamMove : MonoBehaviour {

    [SerializeField]
    Transform target;
    [SerializeField]
    Vector3 distance;

	// Use this for initialization
	void Start () {
        distance = new Vector3(6, 10, -6);
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {

        if(target != null)
        {
            transform.position = target.position + distance;
        }
	}
}
