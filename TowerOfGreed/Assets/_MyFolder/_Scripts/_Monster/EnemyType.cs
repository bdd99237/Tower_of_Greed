using UnityEngine;
using System.Collections;

public class EnemyType : MonoBehaviour {

    public Transform myTransform;
    public float dist;


	// Use this for initialization
	void Start () {

        myTransform = transform.FindChild("HitPoint");

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
