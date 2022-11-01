using UnityEngine;
using System.Collections;

public class EM_SkAttackChk : MonoBehaviour {

    public int _SkillDamage = 10;

    GameObject _Player;
    P_Health _PlayerHP;

    void Awake()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        _PlayerHP = _Player.GetComponent<P_Health>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _PlayerHP._TakeDamage(_SkillDamage);
            Debug.Log("스킬 아파 ㅠ");
            gameObject.SetActive(false);
        }
    }
}
