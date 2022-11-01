using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStatDisply : MonoBehaviour {

    [SerializeField]
    GameObject display;

    [SerializeField]
    Text text_1;
    [SerializeField]
    Text text_2;
    [SerializeField]
    Text text_3;
    [SerializeField]
    Text text_4;
    [SerializeField]
    Text text_5;
    [SerializeField]
    Text text_6;


    // Use this for initialization
    void Start () {
        display.active = false;
	}
	
	// Update is called once per frame
	void Update () {

        text_1.text = "HP : " + GameManager.Instance.result_HP.ToString("0.##");
        text_2.text = "Damge : " + GameManager.Instance.result_DefaultDamge.ToString("0.##");
        text_3.text = "skill_1 : " + GameManager.Instance.result_Skil_1_Damge.ToString("0.##");
        text_4.text = "skill_2 : " + GameManager.Instance.result_Skil_2_Damge.ToString("0.##");
        text_5.text = "Del : " + (GameManager.Instance.del * GameManager.Instance.result_Del).ToString("0.##");
        text_6.text = "Speed : " + GameManager.Instance.result_Speed.ToString("0.##");
    }

    public void OpenDisplay()
    {
        if(display.active)
        {
            display.active = false;
            Time.timeScale = 1.0f;
        }
        else
        {
            display.active = true;
            Time.timeScale = 0f;
        }
    }
}
