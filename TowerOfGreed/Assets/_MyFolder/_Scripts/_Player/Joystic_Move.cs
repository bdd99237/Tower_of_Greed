using UnityEngine;
using System.Collections;

public class Joystic_Move : MonoBehaviour {

    [SerializeField]
    Vector3 startpoint; //스틱의 시작지점

    Vector3 w_startpoint;

    public Vector3 move; //스틱 이동거리 및 이동방향저장
    Vector3 move2;

    int touchInt; //현재 터치 번호
    int touchIntSave; //현재 터치 번호

    Ray ray;
    RaycastHit hit;

    [SerializeField]
    GameObject joyStickObj; //조이스틱오브젝트
    [SerializeField]
    GameObject joyStickObjBg; //스틱의 범위
    [SerializeField]
    BoxCollider box; //조작범위

    [SerializeField]
    GameObject targetObj; //움직일 대상

    bool followObj;
    
    [SerializeField]
    Quaternion camQuat;

    Vector3 camVec;

    Quaternion lookVecter;

    // Use this for initialization
    void Start() {
        w_startpoint = joyStickObjBg.transform.position;
        startpoint = joyStickObjBg.transform.localPosition;
        joyStickObj.transform.localPosition = startpoint;

        if (box != null)
        {
            box.size = new Vector3(Screen.width / 2, Screen.height, 1);
        }

        camQuat = Camera.main.transform.rotation;
        camVec = camQuat.eulerAngles;
        camVec.x = 0;
        camVec.z = 0;
        camQuat = Quaternion.Euler(camVec);

    }

    // Update is called once per frame
    void Update() {

#if (UNITY_EDITOR_WIN)

        if (followObj || Input.GetAxis("Vertical") != 0)
        {
                    PlayerManager.Instance.playerState = PlayerManager.PlayerState.run;
        }
        else
        {
                PlayerManager.Instance.playerState = PlayerManager.PlayerState.stay;
        }

        float rotation = Input.GetAxis("Horizontal") * 500f;
           
            rotation *= Time.deltaTime;
            
        if(targetObj != null)
        {
            targetObj.transform.Rotate(0, rotation, 0);
        }

#else
        if (Input.touchCount > 0)
        {
            if (!followObj)
            {
                if (Input.touchCount == 1)
                {
                    touchInt = 0;
                }
                else if (Input.touchCount > 1)
                {
                    touchInt = 1;
                }
                touchIntSave = touchInt;

                if (Input.GetTouch(touchInt).phase == TouchPhase.Began)
                {
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(touchInt).position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider == box)
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

                if(Input.GetTouch(touchInt).phase == TouchPhase.Moved || Input.GetTouch(touchInt).phase == TouchPhase.Stationary)
                {
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(touchInt).position);
                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider == box)
                        {
                            /*
                        joyStickObj.transform.position = hit.point;
                        joyStickObj.transform.localPosition = new Vector3(joyStickObj.transform.localPosition.x, joyStickObj.transform.localPosition.y, 0); //z축값이 계속바뀌게 되므로 고정시키는 역할
                            
                            move = new Vector3(joyStickObj.transform.localPosition.x - startpoint.x, 0, joyStickObj.transform.localPosition.y - startpoint.y);
                            move = (move.magnitude > 1.0f) ? move.normalized : move;
                            */

                            joyStickObj.transform.position = hit.point;
                            joyStickObj.transform.localPosition = new Vector3(joyStickObj.transform.localPosition.x, joyStickObj.transform.localPosition.y, 0); //z축값이 계속바뀌게 되므로 고정시키는 역할
                            move2 = new Vector3(joyStickObj.transform.localPosition.x - startpoint.x, joyStickObj.transform.localPosition.y - startpoint.y, 0);
                            move2 = (move2.magnitude > 1.0f) ? move2.normalized : move2;
                            joyStickObj.transform.localPosition = move2 + startpoint;

                            move = new Vector3(joyStickObj.transform.localPosition.x - startpoint.x, 0, joyStickObj.transform.localPosition.y - startpoint.y);
                            move = (move.magnitude > 1.0f) ? move.normalized : move;
                        }
                    }
                }
                
                if (targetObj != null)
                {

                    lookVecter = Quaternion.LookRotation(move) * camQuat;
                    // targetObj.transform.localRotation = Quaternion.Slerp(targetObj.transform.localRotation, lookVecter, GameManager.Instance.speed * Time.deltaTime);
                    targetObj.transform.localRotation = lookVecter;
        PlayerManager.Instance.playerState = PlayerManager.PlayerState.run;
                }
            }

            if (Input.GetTouch(touchInt).phase == TouchPhase.Ended && followObj)
            {
                PlayerManager.Instance.playerState = PlayerManager.PlayerState.stay;
                PlayerManager.Instance.StateChange();
                joyStickObj.transform.localPosition = startpoint;
                followObj = false;
                    PlayerManager.Instance.playerState = PlayerManager.PlayerState.stay;
            }
        }

#endif
    }
}
