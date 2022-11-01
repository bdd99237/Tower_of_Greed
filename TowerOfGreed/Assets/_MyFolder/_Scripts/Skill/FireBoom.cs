using UnityEngine;
using System.Collections;

public class FireBoom : MonoBehaviour {

    public float damge;

    public float distance_limits;

    BoxCollider box;

    // Use this for initialization
    void Start () {
        box = GetComponent<BoxCollider>();
        box.size = new Vector3(1, 1, 3);
        box.center = new Vector3(0, 0, box.size.z / 2);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            other.GetComponent<M_HP>()._TakeDamage((int)damge);
            Destroy(gameObject);
        }
    }
}
