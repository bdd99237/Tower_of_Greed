using UnityEngine;
using System.Collections;

public class MA_AtChk : MonoBehaviour
{

    private GameObject _Player;
    private Transform _PlayerTr;
    private Vector3 _PlayerTrTemp;
    public GameObject _Monster;

    private Transform _BTr;
    private Vector3 _BVe;

    public P_Health _Health;
    public float _Damege;

    public float _MoveSpeed = 300.0f;

    void Start()
    {
         
        _Player = GameObject.FindGameObjectWithTag("Player");
        _Health = _Player.GetComponent<P_Health>();
        _PlayerTr = _Player.transform;
        _PlayerTrTemp = _PlayerTr.position;

        _BTr = gameObject.transform;
        //print(transform.position + "\n" + _PlayerTrTemp);

        transform.rotation = Quaternion.LookRotation((_PlayerTrTemp - transform.position).normalized);
    }

    void Update()
    {
        _BVe = transform.forward * _MoveSpeed;



        transform.Translate(_BVe * Time.deltaTime, Space.World);


        Destroy(gameObject, 10.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == _Player)
        {
            _Health._TakeDamage(_Damege);

            Destroy(gameObject);
        }
    }
}
