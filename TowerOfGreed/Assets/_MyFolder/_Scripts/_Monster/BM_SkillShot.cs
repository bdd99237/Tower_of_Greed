using UnityEngine;
using System.Collections;

public class BM_SkillShot : MonoBehaviour
{

    public float _Timer = 0f;
    public float _AttackCount = 0f;

    public float _AttackLimit = 1f;
    public int _SkDamage1 = 50;
    public int _SkDamage2 = 300;

    private GameObject _Player;
    P_Health _PHP;


    void Awake()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
        _PHP = _Player.GetComponent<P_Health>();
        Debug.Log("스킬샷 생성됨");
    }

    void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "BM_LSk2")
        {
            if (other.tag == "Player")
            {
                _PHP._TakeDamage(_SkDamage2);
                Destroy(gameObject, 1.0f);
                Debug.Log("스킬샷2 데미지 줌.");
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (gameObject.tag == "BM_LSk1")
        {
            _AttackCount += Time.deltaTime;
            _Timer += Time.deltaTime;
            if (other.tag == "Player")
            {
                if (_Timer >= _AttackLimit)
                {
                    _PHP._TakeDamage(_SkDamage1);
                    _Timer = 0f;
                    Debug.Log("스킬샷1 데미지 줌");
                }
            }

            if (_AttackCount >= 5.5f)
            {
                Destroy(gameObject);
            }
        }
    }
}
