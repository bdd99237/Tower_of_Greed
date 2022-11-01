using UnityEngine;
using System.Collections;

public class ball_move : MonoBehaviour {

    public Vector3 sponPoint;

    public float distance_limits;

    float distance; //거리측정후 수치저장

    public float speed;
    
    public float damge;

	// Use this for initialization
	void Start () {
        distance_limits = 5.0f;
        speed = 3;
    }
	
	// Update is called once per frame
	void Update () {

        transform.position += transform.forward * speed * Time.deltaTime;

        distance = new Vector3(sponPoint.x - transform.position.x, sponPoint.y - transform.position.y, sponPoint.z - transform.position.z).magnitude;

        if(distance>=distance_limits)
        {
            Destroy(gameObject, 0.5f);
        }

	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
        {
            if(other.GetComponent<M_HP>())
            {
                other.GetComponent<M_HP>()._TakeDamage((int)damge);
                Destroy(gameObject);
            }
        }
    }
}
