using UnityEngine;
using System.Collections;

public enum M_Type
{
    _Long = 0,
    _Short,
    _Boss,
}

public class M_Movement : MonoBehaviour
{
    public M_Type _Type = M_Type._Short;

    GameObject _Player;
    Transform _PlayerTr;
    float _playerDis;

    GameObject _Monster;
    Transform _MonsterTr;
    Vector3 _MonsterTrTemp;
    float _MSs = 5; // 몬스터 인식 범위.
    float _MAs = 2; // 몬스터 공격 범위.

    public UnityEngine.AI.NavMeshAgent nav;

    public Animator _Ani; //애니메이터.

    void Awake()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        _PlayerTr = _Player.transform;

        _Monster = gameObject;
        _MonsterTr = _Monster.transform;
        _MonsterTrTemp = _MonsterTr.position;

        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();

        //애니메이터 컴퍼넌트를 가져옴.
        _Ani = gameObject.GetComponent<Animator>();
        //애니메이터의 플레이 속도 조정.
        _Ani.speed = 1.0f;

        if (_Type == M_Type._Long || _Type == M_Type._Boss)
        {
            _MAs += 3f;
            _MSs += 3f;
        }
    }

    void Update()
    {
        if (_Player != null)
        {
            _playerDis = Vector3.Distance(transform.position, PlayerManager.Instance.player.transform.position); //몬스터와 플레이어간의 거리
        }
        float _HomeDis = Vector3.Distance(transform.position, _MonsterTrTemp); //몬스터 스폰 포지션과 몬스턴 현재 포지션간의 거리

        //플레이어가 인식범위안에 들어왔을 경우.
        if (_playerDis < _MSs)
        {
            if (_Player.GetComponent<P_Health>()._CurrentHP >= 0 && _Monster.GetComponent<M_HP>()._MHpPresent > 0)
            {
                //플레이어가 공격 범위안에 들어왔을 경우.
                if (_playerDis < _MAs)
                {
                    nav.enabled = false;

                    Vector3 Look = _PlayerTr.transform.position + Vector3.up * -1f;

                    _MonsterTr.transform.LookAt(Look);

                    _Ani.SetBool("Run", false);
                    _Ani.SetBool("Attack", true);
                }
                else
                {
                    nav.enabled = true;
                    _Ani.SetBool("Run", true);
                    _Ani.SetBool("Attack", false);
                    nav.SetDestination(_PlayerTr.position);
                }
            }
        }
        //집과의 거리가 인식범위보다 멀 경우.
        else if (_HomeDis > _MSs - 1f)
        {
            if (_Monster.GetComponent<M_HP>()._MHpPresent > 0)
            {
                //집에 도착한 경우 
                if (_HomeDis <= 1)
                {
                    _Ani.SetBool("Run", false);
                    nav.enabled = false;
                }
                else
                {
                    _Ani.SetBool("Run", true);

                    nav.SetDestination(_MonsterTrTemp);

                }
            }
        }

        if (_Player == null)
        {
            nav.enabled = false;
        }
    }
}
