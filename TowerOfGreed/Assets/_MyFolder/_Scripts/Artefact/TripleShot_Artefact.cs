using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TripleShot_Artefact : Attack_Artefact
{

    [SerializeField]
    int terms;
    [SerializeField]
    int counting;

    // Use this for initialization
    void Start()
    {
        base.Start();
        artefact_index = 1;
        type = "Spell_D";
        a_Name = "TripleShot";
        caption = "I'm TripleShot artefact!";
        image = Resources.Load<Sprite>("TripleShot");
        if (count == 0)
        {
            count = 1;
        }

        GetComponent<Image>().sprite = image;

        damge = 10;
        speed = 4.0f;
        dist = 4.0f;
        sponPoint = GameObject.Find("spon").transform;
        Projectiles = Resources.Load<GameObject>("L_Eletric_ball");
        terms = 5;
    }

    void Update()
    {
        base.Update();
    }

    public override void Action()
    {
        List<GameObject> obj = new List<GameObject>();
        obj.Add(Instantiate(Projectiles, sponPoint.position, player.transform.rotation) as GameObject);
        obj.Add(Instantiate(Projectiles, sponPoint.position, player.transform.rotation * Quaternion.Euler(0, 40, 0)) as GameObject);
        obj.Add(Instantiate(Projectiles, sponPoint.position, player.transform.rotation * Quaternion.Euler(0, -40, 0)) as GameObject);

        foreach(GameObject a in obj)
        {
            a.GetComponent<ball_move>().sponPoint = sponPoint.position;
            a.GetComponent<ball_move>().damge = (damge + (count*damge))*1.4f;
            a.GetComponent<ball_move>().speed = speed;
            a.GetComponent<ball_move>().distance_limits = dist;
        }
    }

    public override void InvokeCheck()
    {
        counting += 1;

        if(counting == terms)
        {
            Action();
            counting = 0;
        }
    }
}
