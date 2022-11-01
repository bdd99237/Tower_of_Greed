using UnityEngine;
using System.Collections;

public class M_ActiveColl : MonoBehaviour {

    public GameObject _Player;

    public GameObject _MonsterG;

    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter (Collider other)
    {
        if(other.tag == _Player.tag)
        {
            _MonsterG.SetActive(true);
        }
    }
    void OnTriggerExit (Collider other)
    {
        if(other.tag == _Player.tag)
        {
            gameObject.SetActive(false);
        }
    }
}
