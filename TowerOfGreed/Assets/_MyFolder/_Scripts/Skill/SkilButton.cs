using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkilButton : MonoBehaviour {

    int touchInt; //현재 터치 번호
    int touchIntSave; //현재 터치 번호

    Ray ray;
    RaycastHit hit;

    BoxCollider box_collider; //스틱범위저장.

    [HideInInspector]
    public bool followObj; //이동상태체크

    [SerializeField]
    GameObject targetObj;

    public Skil skilSlot;

    [SerializeField]
    int slotNum;

    float saveTime = 0;

    [SerializeField]
    float coolTime;

    float coolNum = 0;

    Image buttonImage;

    Text coolTimeText;

    // Use this for initialization
    void Start()
    {
        targetObj = GameObject.FindGameObjectWithTag("Player");
        box_collider = GetComponent<BoxCollider>();
        buttonImage = GetComponent<Image>();
        coolTimeText = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
#if (UNITY_EDITOR_WIN)
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (gameObject.name == "Skill_1")
            {
                PlayerManager.Instance.playerState = PlayerManager.PlayerState.slot_1;
                skilSlot.action(targetObj);
            }

        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            if (gameObject.name == "Skill_2")
            {
                PlayerManager.Instance.playerState = PlayerManager.PlayerState.slot_2;
                skilSlot.action(targetObj);
            }
        }

#else
        if (saveTime < (coolTime * GameManager.Instance.result_Del))
        {
            saveTime += Time.deltaTime;
        }
        else
        {
            saveTime = coolTime;
        }

        //터치가 1개이상있다면.
        if (Input.touchCount > 0)
        {
            if (!followObj)
            {
                //터치가 1개라면 0번터치, 2개이상이라면 1번터치
                if (Input.touchCount == 1)
                {
                    touchInt = 0;
                }
                else if (Input.touchCount > 1)
                {
                    touchInt = 1;
                }
                touchIntSave = touchInt; //저장시켜둔다.

                if (Input.GetTouch(touchInt).phase == TouchPhase.Began)
                {
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(touchInt).position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider == box_collider)
                        {
                            followObj = true;
                        }
                    }
                }
            }
            else
            {
                if (Input.touchCount == 1)
                {
                    touchInt = 0;
                }
                else if (Input.touchCount == 2)
                {
                    touchInt = touchIntSave;
                }

                if (targetObj != null)
                {
                    if (saveTime >= (coolTime * GameManager.Instance.result_Del))
                    {
                        if (GameManager.Instance.artefact.Count != 0)
                        {
                            foreach (Artefact obj in GameManager.Instance.artefact)
                            {
                                if (obj.GetComponent<Artefact>().type == "Spell_DS")
                                {
                                    obj.GetComponent<Artefact>().InvokeCheck();
                                }
                            }
                        }

                        if (slotNum == 0)
                        {
                            PlayerManager.Instance.playerState = PlayerManager.PlayerState.slot_1;
                        }
                        else if (slotNum == 1)
                        {
                            PlayerManager.Instance.playerState = PlayerManager.PlayerState.slot_2;
                        }
                        skilSlot.action(targetObj);
                        saveTime = 0;
                    }

                    if (Input.GetTouch(touchInt).phase == TouchPhase.Ended && followObj)
                    {
                        followObj = false;
                        PlayerManager.Instance.playerState = PlayerManager.PlayerState.stay;
                    }
                }
            }
        }

            coolNum = (coolTime * GameManager.Instance.result_Del) - saveTime;

            buttonImage.fillAmount = 1 - coolNum / (coolTime * GameManager.Instance.result_Del);

            if (coolNum <= 0)
            {
                coolTimeText.enabled = false;
            }
            else
            {
                coolTimeText.enabled = true;
                coolTimeText.text = (coolNum + 0.5).ToString("0");
            }
#endif
    }
}