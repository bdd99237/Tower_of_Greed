using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stat_Artefact : Artefact {

    public float hp;
    public float attackPoint;
    public float skil_Attack;
    public float attackSpeed;
    public float moveSpeed;

    void Start () {
        base.Start();
        artefact_index = 0;
        type = "StatType";
        a_Name = "All State";
        caption = "I'm all state artefact!";
        image = Resources.Load<Sprite>("StatImage");
        if (count == 0)
        {
            count = 1;
        }

        hp = 50.0f;
        attackPoint = 5.0f;
        skil_Attack = 20.0f;
        attackSpeed = 0.05f;
        moveSpeed = 1.0f;

        GetComponent<Image>().sprite = image;
    }

    void Update()
    {
        base.Update();
    }

    public override void Action()
    {
        GameManager.Instance.increase_HP = hp;
        GameManager.Instance.increase_defaultDamge = attackPoint;
        GameManager.Instance.increase_Skil_Damge = skil_Attack;
        GameManager.Instance.increase_Del = attackSpeed;
        GameManager.Instance.increase_Speed = moveSpeed;
    }
}
