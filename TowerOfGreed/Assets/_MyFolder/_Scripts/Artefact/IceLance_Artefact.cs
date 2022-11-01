using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IceLance_Artefact : Attack_Artefact
{
    [SerializeField]
    int terms;
    [SerializeField]
    int counting;

    // Use this for initialization
    void Start()
    {
        base.Start();
        artefact_index = 2;
        type = "Spell_DS";
        a_Name = "IceLance";
        caption = "I'm IceLance artefact!";
        image = Resources.Load<Sprite>("IceLance");
        if (count == 0)
        {
            count = 1;
        }

        GetComponent<Image>().sprite = image;

        damge = 20;
        speed = 3.5f;
        dist = 4.0f;
        sponPoint = GameObject.Find("spon").transform;
        Projectiles = Resources.Load<GameObject>("FireBoom");
        terms = 8;
    }

    void Update()
    {
        base.Update();
    }

    public override void Action()
    {
        GameObject obj = Instantiate(Projectiles, sponPoint.position, player.transform.rotation) as GameObject;
        obj.GetComponent<FireBoom>().damge = (damge + (count * damge)) * 1.4f;
        obj.GetComponent<FireBoom>().distance_limits = dist;
        }

    public override void InvokeCheck()
    {
        counting = Random.RandomRange(0, 100+1); //max-1까지만 돌기때문에 max에 +1해줘야한다. 일부러 100+1함
        if(counting<=terms)
        {
            Action();
        }
        counting = 0;
    }
}
