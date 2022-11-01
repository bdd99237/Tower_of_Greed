using UnityEngine;
using System.Collections;

public class playerprefeb : MonoBehaviour {
    [SerializeField]
    GameObject player;

	// Use this for initialization
	void Start () {
	if(GameManager.Instance.playerClass == "Warrior")
        {
            player = Instantiate(Resources.Load<GameObject>("Warrior"), transform.position, Quaternion.identity) as GameObject;
        }
    else if(GameManager.Instance.playerClass == "Magician")
        {
            player = Instantiate(Resources.Load<GameObject>("mage"), transform.position, Quaternion.identity) as GameObject;
        }
        player.transform.parent = GameObject.Find("OB_Player").transform;
        player.transform.localScale = new Vector3(1, 1, 1);
        player.transform.localPosition = new Vector3(0, -0.65f, 0);
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
