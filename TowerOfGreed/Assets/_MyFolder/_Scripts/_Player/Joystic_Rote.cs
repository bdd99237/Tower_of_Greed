using UnityEngine;
using System.Collections;

public class Joystic_Rote : MonoBehaviour
{

    #region 이전코드
    /*
    int touchInt; //현재 터치 번호
    int touchIntSave; //현재 터치 번호

    Ray ray;
    RaycastHit hit;
    
    [SerializeField]
    GameObject joyStickObjBg; //스틱의 범위
    BoxCollider collider; //스틱범위저장.

    [SerializeField]
    GameObject targetObj; //움직일 대상

        [HideInInspector]
    public bool followObj; //이동상태체크

    // Use this for initialization
    void Start()
    {
            collider = joyStickObjBg.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
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
                        if (hit.collider == collider)
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

                if (Input.GetTouch(touchInt).phase == TouchPhase.Moved || Input.GetTouch(touchInt).phase == TouchPhase.Stationary)
                {
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(touchInt).position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider == collider)
                        {

                        }
                    }
                }
               
                if (targetObj != null)
                {

                }
            }

            if (Input.GetTouch(touchInt).phase == TouchPhase.Ended && followObj)
            {
                followObj = false;
            }
        }
    }
    */
    #endregion

    int touchInt; //현재 터치 번호
    int touchIntSave; //현재 터치 번호

    Ray ray;
    RaycastHit hit;

    [SerializeField]
    GameObject joyStickObjBg; //스틱의 범위

    BoxCollider bg_collider; //스틱범위저장.

    [HideInInspector]
    public bool followObj; //이동상태체크

    [SerializeField]
    GameObject target;

    // Use this for initialization
    void Start()
    {
        bg_collider = joyStickObjBg.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
#if (UNITY_EDITOR_WIN)
        if (Input.GetKeyDown(KeyCode.F3))
        {
            PlayerManager.Instance.playerState = PlayerManager.PlayerState.default_Attack;
            followObj = true;
        }
        else
        {
            followObj = false;
        }

#else
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
                        if (hit.collider == bg_collider)
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
            }

            if (Input.GetTouch(touchInt).phase == TouchPhase.Ended && followObj)
            {
                followObj = false;
            }
        }
#endif
    }
}
