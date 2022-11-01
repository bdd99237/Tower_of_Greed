using UnityEngine;
using System.Collections;

public class P_Attack : MonoBehaviour {

    [SerializeField]
    GameObject eletricBall;

    [SerializeField]
    GameObject sword;

    [SerializeField]
    float saveTime;

    [SerializeField]
    Joystic_Rote rightJoy;

    public Transform sponPoint;

	// Use this for initialization
	void Start () {

        if (GameManager.Instance.playerClass == "Warrior")
        {
            sword = GameObject.Find("sword02");
        }
        else if (GameManager.Instance.playerClass == "Magician")
        {
            eletricBall = Resources.Load<GameObject>("L_Eletric_ball");
        }
	}

    // Update is called once per frame
    void Update() {
        if (saveTime < GameManager.Instance.del) { saveTime += Time.deltaTime; }
        
        if (rightJoy.followObj)
        {
            if (saveTime > (GameManager.Instance.del * GameManager.Instance.result_Del))
            {

                PlayerManager.Instance.playerState = PlayerManager.PlayerState.default_Attack;

                if(GameManager.Instance.artefact.Count != 0)
                {
                    foreach (Artefact obj in GameManager.Instance.artefact)
                    {
                        if (obj.GetComponent<Artefact>().type == "Spell_D" || obj.GetComponent<Artefact>().type == "Spell_DS")
                        {
                            obj.GetComponent<Artefact>().InvokeCheck();
                        }
                    }
                }
                
                if (GameManager.Instance.playerClass == "Warrior")
                {
                    sword.GetComponent<sword>().damge = GameManager.Instance.result_DefaultDamge;
                    sword.GetComponent<sword>().isAttack = true;
                }
                else if(GameManager.Instance.playerClass == "Magician")
                {
                    GameObject basicBall = Instantiate(eletricBall, sponPoint.position, transform.rotation) as GameObject;
                    basicBall.GetComponent<ball_move>().sponPoint = sponPoint.position;
                    basicBall.GetComponent<ball_move>().damge = GameManager.Instance.result_DefaultDamge;
                }
                saveTime = 0;
            }
        }
	}
}
