using UnityEngine;
using System.Collections;

public class M_Attack : MonoBehaviour
{
    public int _AttackDamage1 = 5; //공격 데미지.

    public int _AttackStack = 0; //공격 스택.
    public int _AttackLimit = 0; //공격 제한 스택.

    GameObject _Player; //플레이어 오브젝트.
    Transform _PlayerTr; //플레이어 트랜스폼.

    P_Health _PlayerHP; //플레이어 HP 스크립트.

    Transform _MTr; //몬스터 트랜스폼.

    public GameObject _Bullet; //원거리 몬스터 투사체.
    public GameObject _SkBullet; //원거리 몬스터 스킬 투사체.
    public GameObject _BMSk1; //보스 몬스터 스킬 오브젝트1.
    public GameObject _BMSk2; //보스 몬스터 스킬 오브젝트2.
    public int _BMSkChk = 0; //보스 몬스터 스킬 체크.

    public Transform BulletStart; //총알 발사 지점.

    public Animator _Ani; //애니메이션.

    M_Type _type;

    void Awake()
    {
        _AttackLimit = Random.Range(4, 7);

        _Player = GameObject.FindGameObjectWithTag("Player");
        _PlayerTr = _Player.transform;

        _MTr = gameObject.transform;

        //플레이어 HP 스크립트 호출.
        _PlayerHP = _Player.GetComponent<P_Health>();

        //애니메이터 컴퍼넌트를 가져옴.
        _Ani = gameObject.GetComponent<Animator>();
        //애니메이터의 플레이 속도 조정.
        _Ani.speed = 1.0f;

        _type = GetComponent<M_Movement>()._Type;
    }

    public void _Attack()
    {
        //일반몹 근거리
        if (_type == M_Type._Short)
        {
            if (_PlayerHP._CurrentHP > 0)
            {
                _PlayerHP._TakeDamage(_AttackDamage1);
            }
        }

        //일반몹 원거리
        else if (_type == M_Type._Long)
        {
            if (_PlayerHP._CurrentHP > 0)
            {

                //총알 발사.
                if (_AttackLimit == _AttackStack)
                {
                    _BulletFire(2);

                    //스택 초기화.
                    _AttackStack = 0;

                    //스택 제한 초기화. 
                    _AttackLimit = Random.Range(4, 7);
                }
                else
                {
                    _AttackStack++;

                    _BulletFire(1);
                }
            }
        }

        //보스몹 원거리
        else if (_type == M_Type._Boss)
        {
            if (_PlayerHP._CurrentHP > 0)
            {
                M_HP _MHp;
                _MHp = GetComponent<M_HP>();

                Debug.Log("체력 백분율" + _MHp._MHpPresent / _MHp._MHp );
                //체력 70% 이하시 발동.
                if (_MHp._MHpPresent / _MHp._MHp <= 0.7f && _MHp._MHpPresent / _MHp._MHp > 0.3f && _BMSkChk == 0)
                {
                    _BulletFire(3);
                    _BMSkChk++;
                    Debug.Log("스킬샷");
                }
                //체력 30% 이하시 발동.
                else if (_MHp._MHpPresent / _MHp._MHp <= 0.3f && _MHp._MHpPresent / _MHp._MHp > 0.1f && _BMSkChk == 1)
                {
                    _BulletFire(4);
                    _BMSkChk++;
                    Debug.Log("스킬샷2");
                }
                //총알 발사.
                else if (_AttackLimit == _AttackStack)
                {
                    _BulletFire(2);

                    //스택 초기화.
                    _AttackStack = 0;

                    //스택 제한 초기화. 
                    _AttackLimit = Random.Range(4, 7);
                }
                else
                {
                    _AttackStack++;

                    _BulletFire(1);
                }
            }
        }
    }

    void _BulletFire(int num)
    {
        if (_type == M_Type._Long)
        {
            //발사 좌표.
            BulletStart = transform.FindChild("RigPelvis").FindChild("RigSpine1").FindChild("RigSpine2").FindChild("RigRibcage").FindChild("RigRArm1").FindChild("RigRArm2").FindChild("RigRArmPalm").FindChild("Staff").FindChild("Aura");
        }
        else if(_type == M_Type._Boss)
        {
            //발사 좌표.
            BulletStart = transform.FindChild("Rig").FindChild("Dummy Rock All").FindChild("Dummy Rock 05");
        }
        GameObject newBullet = null;

        if (num == 1)
        {
            //총알 생성.
            newBullet = Instantiate(_Bullet) as GameObject; // 미사일 오브젝트 생성.

            newBullet.GetComponent<MA_AtChk>()._Health = _PlayerHP;
            newBullet.GetComponent<MA_AtChk>()._Damege = _AttackDamage1;

            newBullet.GetComponent<MA_AtChk>()._Monster = BulletStart.gameObject;

            //총알 생성 위치.
            newBullet.transform.position = BulletStart.position;
        }
        else if (num == 2)
        {
            //총알 생성.
            newBullet = Instantiate(_SkBullet) as GameObject; // 미사일 오브젝트 생성.

            newBullet.GetComponent<MA_AtChk>()._Health = _PlayerHP;
            newBullet.GetComponent<MA_AtChk>()._Damege = _AttackDamage1;

            newBullet.GetComponent<MA_AtChk>()._Monster = BulletStart.gameObject;

            //총알 생성 위치.
            newBullet.transform.position = BulletStart.position;
        }
        else if (num == 3)
        {
            //총알 생성.
            newBullet = Instantiate(_BMSk1) as GameObject; // 미사일 오브젝트 생성.

            //총알 생성 위치.
            newBullet.transform.position = _PlayerTr.position;

            Debug.Log("스킬샷 생성.");
        }
        else if (num == 4)
        {
            //총알 생성.
            newBullet = Instantiate(_BMSk2) as GameObject; // 미사일 오브젝트 생성.

            //총알 생성 위치.
            newBullet.transform.position = _MTr.position;

            Debug.Log("스킬샷2 생성");
        }
    }
}