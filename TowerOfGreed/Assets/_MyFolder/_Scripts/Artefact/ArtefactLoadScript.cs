using UnityEngine;
using System.Collections;

public class ArtefactLoadScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(GameManager.Instance.isArtefactLoad)
        {
            GameManager.Instance.Load_Artefact(GameManager.Instance.loadSlotNum);
            GameManager.Instance.isArtefactLoad = false;
        }
    else
        {
            Destroy(gameObject);
        }

	}
}
