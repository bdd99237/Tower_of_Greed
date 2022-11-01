using UnityEngine;
using System.Collections;

public class EarthQuake : Skil {

    BoxCollider colliderBox;

    void Awake()
    {
        
    }

    void Start()
    {
        

        colliderBox = GetComponent<BoxCollider>();
        colliderBox.size = new Vector3(3, 1, 3);
        Destroy(gameObject, 1.0f);
    }

    public override void action(GameObject player)
    {
        damge = GameManager.Instance.result_Skil_1_Damge;
        dist = 3;
        anim_Name = "pc_skill_buff";
        GameObject bolt =Instantiate(gameObject, player.GetComponent<P_Attack>().sponPoint.position + (player.transform.forward * dist), player.transform.rotation) as GameObject;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Player")
        {
            other.GetComponent<M_HP>()._TakeDamage((int)damge);
        }
    }
}
