using UnityEngine;
using System.Collections;

public class BM_Attack : MonoBehaviour {

    public float _AttackDelay = 1f;
    public int _AttackDamage1 = 5;
    public int _AttackChk = 0;
    public int _AttackLimit = 2;
    float _Timer = 1f;
    private bool _PlayerInRange;

    GameObject _Player;
    Transform _PlayerTr;

    P_Health _PlayerHP;

    GameObject _BMName; //몬스터 종류.
    Transform _BMTr; //몬스터 트랜스폼.
    Vector3 _BMPo;//몬스터 포지션.

    public GameObject _Bullet; //몬스터 투사체.
    public GameObject _SkBullet; //몬스터 스킬 투사체.

    public GameObject _BMSkill1; //보스 원거리 몬스터 스킬 투사체.
    public GameObject _BMSkill2; //보스 원거리 몬스터 스킬 투사체.

    M_HP _MHP;

    public int _BMHPChk1 = 70;
    public int _BMHPChk2 = 30;

    void Awake()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        _PlayerTr = _Player.transform;

        _PlayerHP = _Player.GetComponent<P_Health>();
        _MHP = gameObject.GetComponent<M_HP>();

        _BMName = gameObject;
        _BMTr = _BMName.transform;
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
        }
    }

    void Update()
    {
        _Timer += Time.deltaTime;

        if (_Timer >= _AttackDelay && _PlayerInRange)
        {
            _Attack();
        }


        if (_PlayerHP._CurrentHP <= 0)
        {

        }
    }

    void _Attack()
    {
        //보스 근거리
        if (_BMName == _BMName.CompareTag("BM_Short"))
        {
            if (_MHP._MHpPresent <= _BMHPChk1)
            {

            }
            else if (_AttackChk < _AttackLimit)
            {
                if (_PlayerHP._CurrentHP > 0)
                {
                    _PlayerHP._TakeDamage(_AttackDamage1);
                    ++_AttackChk;

                    _Timer = 0f;
                }
            }
            else if (_AttackChk >= _AttackLimit)
            {
                if (_PlayerHP._CurrentHP > 0)
                {
                    _BMName.transform.FindChild("SkillAttackCollider").gameObject.SetActive(true);
                    _AttackChk = 0;

                    _Timer = 0f;
                }
            }
        }

        //보스 원거리
        else if (_BMName == _BMName.CompareTag("BM_Long"))
        {
            if ((_MHP._MHpPresent / _MHP._MHp <= _BMHPChk1) && (_MHP._MHpPresent / _MHP._MHp > _BMHPChk2))
            {
                Instantiate(_BMSkill1, _PlayerTr.position, _PlayerTr.rotation);
            }

            else if((_MHP._MHpPresent / _MHP._MHp <= _BMHPChk2) && (_MHP._MHpPresent / _MHP._MHp > 1))
            {
                _BMPo = _BMTr.position; //몬스터 포지션 저장.

                Instantiate(_BMSkill2, _BMPo, Quaternion.Euler(0f,0f,0f));
            }

            if (_AttackChk < _AttackLimit)
            {
                if (_PlayerHP._CurrentHP > 0)
                {
                    _BMPo = _BMTr.position; //몬스터 포지션 저장.

                    transform.LookAt(_PlayerTr.position);
                    Instantiate(_Bullet, _BMPo, _BMTr.rotation); // 미사일 오브젝트 생성.
                    ++_AttackChk;

                    _Timer = 0f;
                }
            }
            else if (_AttackChk >= _AttackLimit)
            {
                if (_PlayerHP._CurrentHP > 0)
                {
                    _BMPo = _BMTr.position; //몬스터 포지션 저장.

                    transform.LookAt(_PlayerTr.position);
                    Instantiate(_SkBullet, _BMPo, _BMTr.rotation); //미사일 오브젝트 생성.

                    //초기화 변수
                    _AttackChk = 0;
                    _Timer = 0f;
                }
            }
        }
    }
}
