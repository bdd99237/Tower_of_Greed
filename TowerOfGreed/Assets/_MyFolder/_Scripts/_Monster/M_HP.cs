using UnityEngine;
using System.Collections;

public class M_HP : MonoBehaviour {

    public float _MHp;
    public float _MHpPresent;

    public Animator _Ani; //애니메이터.

    M_Movement M_m;

    void Awake ()
    {
        //애니메이터 컴퍼넌트를 가져옴.
        _Ani = gameObject.GetComponent<Animator>();
        //애니메이터의 플레이 속도 조정.
        _Ani.speed = 1.0f;

        //네브매쉬 호출.
        M_m = gameObject.GetComponent<M_Movement>();
    }

    void Start ()
    {
        _MHpPresent = _MHp;
    }

    public void _TakeDamage(int _Dmg)
    {
        _MHpPresent -= _Dmg;

        if (_MHpPresent <= 0)
        {
            _Ani.SetBool("Die",true);
            _MHpPresent = 0;
        }
    }

    void _Death()
    {
        M_m.nav.enabled = false;
        Destroy(gameObject, 1f);
    }
}