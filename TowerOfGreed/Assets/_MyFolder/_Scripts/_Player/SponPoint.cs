using UnityEngine;
using System.Collections;

public class SponPoint : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectWithTag("Player");

        WarpManager.Instance.StageWarp(player.GetComponent<Collider>());

        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
