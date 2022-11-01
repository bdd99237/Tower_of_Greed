using UnityEngine;
using System.Collections;

public class Spon_Warp : MonoBehaviour {
    Transform point;

	// Use this for initialization
	void Start () {
        point = transform;
	}
	
	// Update is called once per frame
	void Update () {

        if(GetComponent<M_HP>()._MHpPresent <= 0)
        {
            Instantiate(Resources.Load("Warp"),point.position,point.rotation);
            GetComponent<Spon_Warp>().enabled = false;
        }
    }
}
