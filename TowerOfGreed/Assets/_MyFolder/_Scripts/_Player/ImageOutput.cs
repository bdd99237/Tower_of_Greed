using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageOutput : MonoBehaviour {

    Image characterImage;

    // Use this for initialization
    void Start () {

        characterImage = GetComponent<Image>();

	}
	
	// Update is called once per frame
	void Update () {
	if(GameManager.Instance.playerClass == "Warrior")
        {
            characterImage.sprite = Resources.Load<Sprite>("warrior_all");
        }
    else if(GameManager.Instance.playerClass == "Magician")
        {
            characterImage.sprite = Resources.Load<Sprite>("mageion_all");
        }
    else
        {
            characterImage.sprite = null;
        }
	}
}
