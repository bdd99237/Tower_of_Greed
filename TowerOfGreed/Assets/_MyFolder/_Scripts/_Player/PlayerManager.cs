using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    #region 싱글톤
    private static PlayerManager _instance = null;

    public static PlayerManager Instance{
        get {
            if (_instance == null) { }
            return _instance; } }

    private PlayerManager(){ }
    #endregion

    public enum PlayerState
    {
        stay,
        run,
        default_Attack,
        slot_1,
        slot_2,
        dead
    }

    public PlayerState playerState = PlayerState.stay;
    public PlayerState temp = PlayerState.stay;


    public GameObject player;

    [SerializeField]
    Animator anim;

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

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update() {
        if (player == null)
        {
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                player = GameObject.FindGameObjectWithTag("Player");
                anim = player.GetComponentInChildren<Animator>();
            }
        }
        else
        {
            /*
            if (player.GetComponentInChildren<Animation>().isPlaying && !ismove && playerState == PlayerState.run) //달리던중 멈출때.(애니플레이중이며, 이동버튼을 안눌렀고, 뛰던중이라면)
            {
                playerState = PlayerState.stay;
            }
            else if(!player.GetComponentInChildren<Animation>().isPlaying)
            {
                playerState = PlayerState.stay;
            }
            */

            StateChange();
        }
        Debug.Log("현재 상태 : " + playerState);
	}

    public void StateReset()
    {
        if(temp != playerState)
        {
            /*
            anim.SetBool("isRun", false);
            anim.SetBool("isDead", false);
            anim.SetBool("isAttack", false);
            anim.SetBool("isSkill1", false);
            anim.SetBool("isSkill2", false);
            */
            player.GetComponentInChildren<P_Move>().Stop();
            temp = playerState;
        }
    }

    public void StateChange()
    {
        StateReset();

        switch (playerState)
        {
            case PlayerState.stay:
                anim.SetInteger("isState", 0);
                break;

            case PlayerState.run:
                player.GetComponentInChildren<P_Move>().Go();
                //anim.SetBool("isRun", true);
                anim.SetInteger("isState", 1);
                break;

            case PlayerState.default_Attack:
                //anim.SetBool("isAttack", true);
                anim.SetInteger("isState", 2);
                break;

            case PlayerState.slot_1:
                //anim.SetBool("isSkill1", true);
                anim.SetInteger("isState", 3);
                break;

            case PlayerState.slot_2:
                //anim.SetBool("isSkill2", true);
                anim.SetInteger("isState", 4);
                break;

            case PlayerState.dead:
                //anim.SetBool("isDead", true);
                anim.SetInteger("isState", 5);
                break;

            default:
                //Debug.Log("정상적인 값이 아닙니다.");
                break;
        }
    }

    public void AddArtefectScript(GameObject target ,int num)
    {
        switch (num)
        {
            case 0:
                target.AddComponent<Stat_Artefact>();
                break;

            case 1:
                target.AddComponent<TripleShot_Artefact>();
                break;

            case 2:
                target.AddComponent<IceLance_Artefact>();
                break;

            default:
                break;
        }
    }
}
