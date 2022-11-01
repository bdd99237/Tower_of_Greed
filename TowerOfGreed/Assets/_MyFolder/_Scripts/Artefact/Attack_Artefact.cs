using UnityEngine;
using System.Collections;


public class Attack_Artefact : Artefact
{

    public float damge;
    public float speed;
    public float dist;

    public Transform sponPoint;
    public GameObject Projectiles;
    public GameObject player;

    // Use this for initialization
    public virtual void Start () {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        base.Update();
    }
}
