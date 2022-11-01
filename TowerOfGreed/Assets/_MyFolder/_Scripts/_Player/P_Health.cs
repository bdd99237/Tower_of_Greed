using UnityEngine;
using System.Collections;

public class P_Health : MonoBehaviour {
    public float _CurrentHP;
    public float _MaxHP;

    bool _IsDead;
    bool _Damaged;

    [SerializeField]
    Transform hp_bar;

    void Awake ()
    {
        _MaxHP = GameManager.Instance.result_HP;
        _CurrentHP = _MaxHP;
        hp_bar = GameObject.Find("HP_Bar_Fill").transform;
    }
	
	void Update ()
    {
        hp_bar.localScale = new Vector3(_CurrentHP/_MaxHP,1,1);

        _Damaged = false;
	}

   
    public void _TakeDamage (float _Dmg)
    {
        _Damaged = true;
        _CurrentHP -= _Dmg;

        if(_CurrentHP <= 0 && !_IsDead)
        {
            PlayerManager.Instance.playerState = PlayerManager.PlayerState.dead;
            _Death();
        }
    }

    void _Death()
    {
        _IsDead = true;

        Destroy(gameObject, 1);
    }
}
