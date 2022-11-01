using UnityEngine;
using System.Collections;

public class EM_Attack : MonoBehaviour {

    public float _AttackDelay = 1f;
    public int _AttackDamage1 = 5;
    public int _AttackChk = 0;
    public int _AttackLimit = 2;
    float _Timer = 1f;
    private bool _PlayerInRange;

    public int _AtLimitMin = 3;
    public int _AtLimitMax = 5;

    GameObject _Player;
    Transform _PlayerTr;

    P_Health _PlayerHP;

    GameObject _EMName; //몬스터 종류.
    Transform _EMTr; //몬스터 트랜스폼.
    Vector3 _EMPo;//몬스터 포지션.

    public GameObject _Bullet; //몬스터 투사체.
    public GameObject _SkBullet; //몬스터 스킬 투사체.
    public Transform BulletStart;

    public Animator _Ani;

    void Awake()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        _PlayerTr = _Player.transform;

        _PlayerHP = _Player.GetComponent<P_Health>();

        _EMName = gameObject;
        _EMTr = _EMName.transform;

        //애니메이터 컴퍼넌트를 가져옴.
        _Ani = gameObject.GetComponent<Animator>();
        //애니메이터의 플레이 속도 조정.
        _Ani.speed = 1.0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _Player)
        {
            _PlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _Player)
        {
            _PlayerInRange = false;
            _Ani.SetBool("Melee Attack", false);
        }
    }

    void Update()
    {
        _Timer += Time.deltaTime;
        //Debug.Log(_Timer);
        if (_Timer >= _AttackDelay && _PlayerInRange)
        {
            _Attack();
        }


        if (_PlayerHP._CurrentHP <= 0)
        {
            //Debug.Log("플레이어 죽음.");
        }
    }

    void _Attack()
    {
        //엘리트 근거리
        if (_EMName == _EMName.CompareTag("EM_Short"))
        {
            if (_AttackChk < _AttackLimit)
            {
                if (_PlayerHP._CurrentHP > 0)
                {
                    _PlayerHP._TakeDamage(_AttackDamage1);
                    ++_AttackChk;

                    _Timer = 0f;
                    Debug.Log("첫번째 Short 패턴.");
                }
            }
            else if (_AttackChk >= _AttackLimit)
            {
                if (_PlayerHP._CurrentHP > 0)
                {
                    _EMName.transform.FindChild("SkillAttackCollider").gameObject.SetActive(true);
                    _AttackChk = 0;

                    _Timer = 0f;
                    Debug.Log("두번째 Short 패턴.");
                }
            }
        }

        //엘리트 원거리
        else if (_EMName == _EMName.CompareTag("EM_Long"))
        {
            if (_AttackChk < _AttackLimit)
            {
                if (_PlayerHP._CurrentHP > 0)
                {
                    _EMPo = _EMTr.position; //몬스터 포지션 저장.

                    _Ani.SetBool("Melee Attack", true);

                    //Instantiate(_Bullet, _EMPo, _EMTr.rotation); // 미사일 오브젝트 생성.

                    BulletStart = transform.FindChild("RigPelvis").FindChild("RigSpine1").FindChild("RigSpine2").FindChild("RigRibcage").FindChild("RigRArm1").FindChild("RigRArm2").FindChild("RigRArmPalm").FindChild("Staff").FindChild("Aura");
                    GameObject newBullet = Instantiate(_Bullet) as GameObject; // 미사일 오브젝트 생성.
                    newBullet.transform.position = BulletStart.position;

                    newBullet.GetComponent<MA_AtChk>()._Monster = BulletStart.gameObject;

                    ++_AttackChk;

                    _Timer = 0f;
                    //Debug.Log("첫번째 Long 패턴.");
                }
            }
            else if (_AttackChk >= _AttackLimit)
            {
                if (_PlayerHP._CurrentHP > 0)
                {
                    _EMPo = _EMTr.position; //몬스터 포지션 저장.

                    _Ani.SetBool("Melee Attack", true);

                    //Instantiate(_SkBullet, _EMPo, _EMTr.rotation); //미사일 오브젝트 생성.

                    BulletStart = transform.FindChild("RigPelvis").FindChild("RigSpine1").FindChild("RigSpine2").FindChild("RigRibcage").FindChild("RigRArm1").FindChild("RigRArm2").FindChild("RigRArmPalm").FindChild("Staff").FindChild("Aura");
                    GameObject newBullet = Instantiate(_SkBullet) as GameObject; // 미사일 오브젝트 생성.
                    newBullet.transform.position = BulletStart.position;

                    newBullet.GetComponent<MA_AtChk>()._Monster = BulletStart.gameObject;

                    //초기화 변수
                    _AttackChk = 0;
                    _AttackLimit = Random.Range(_AtLimitMin, _AtLimitMax + 1);
                    _Timer = 0f;
                    //Debug.Log("두번째 Long 패턴.");
                }
            }
        }
    }
}
