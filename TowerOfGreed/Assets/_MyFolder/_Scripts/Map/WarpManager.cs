using UnityEngine;
using System.Collections;

public class WarpManager : MonoBehaviour
{

    #region 싱글톤
    private static WarpManager _instance = null;

    public static WarpManager Instance
    {
        get
        {
            if (_instance == null) { }
            return _instance;
        }
    }

    private WarpManager() { }
    #endregion

    public RoomInpo[] sponPoint; //플레이어 소환위치

    void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject); //이 스크립트를 가진 오브젝트가 파괴되지 않는다.
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    public void StageWarp(Collider player)
    {
        if (GameManager.Instance.playerStage % 5 == 0)
        {
            for(int i = 0; i<sponPoint.Length; i++)
            {
                if(sponPoint[i].roomName == "Boss")
                {
                    player.transform.position = sponPoint[i].position;
                }
            }
        }
        else
        {
            int num = Random.RandomRange(0, sponPoint.Length);

            while (sponPoint[num].roomName == "Boss")
            {
                num = Random.RandomRange(0, sponPoint.Length);
            }

                player.transform.position = sponPoint[num].position;
        }
    }
}

[System.Serializable] //커스텀 클래스를 인스펙터로 나오도록하는 명령
public class RoomInpo
{
    public string roomName;
    public Vector3 position;
}
