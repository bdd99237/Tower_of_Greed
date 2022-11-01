using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayStage : MonoBehaviour {

    [SerializeField]
    Text text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        text.text = GameManager.Instance.playerStage.ToString();
	}
}
