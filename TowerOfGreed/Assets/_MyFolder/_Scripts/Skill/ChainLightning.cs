using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChainLightning : Skil {
    public GameObject tailPrefab; //타격대상에 주는 이펙트
    public GameObject linePrefab; //타격대상사이의 전격이펙트
    [SerializeField]
    List<EnemyType> enemy = new List<EnemyType>();
    [SerializeField]
    List<EnemyType> enemies = new List<EnemyType>(); //타격대상을 저장하는 부분.

    public int enemyLimite = 0; //타격대상의 수를 제한

    public float fDist; //첫타격시의 공격사거리

    [SerializeField]
    float attackDelay;
    [SerializeField]
    float saveTime;
    [SerializeField]
    int attackCount;
    [SerializeField]
    int saveCount;

    public GameObject target;
    public GameObject player;
    bool test;


    //public Animation ani;
    public Animator ani;
    public float a;
    public float b;

    void Awake()
    {
        //프리펩을 불러옴
        tailPrefab = Resources.Load<GameObject>("LightningBoltTail");
        linePrefab = Resources.Load<GameObject>("LightningBoltLine");
        anim_Name = "pc_skill_01";
    }

    void Start()
    {
        //데미지 지정
        damge = GameManager.Instance.result_Skil_2_Damge;

        enemyLimite = 7;

        fDist = 4;
        dist = 4;

        //타격 간격 지정
        attackDelay = 0.5f;
        //타격 횟수 지정
        attackCount = 6;

        saveCount = 0;
        saveTime = 0;
    }

    void Update()
    {
            StartCoroutine("Lightning");
    }

    public override void action(GameObject player)
    {
        GameObject chainLightningClon = Instantiate(gameObject, player.transform.position, player.transform.rotation) as GameObject;
        chainLightningClon.GetComponent<ChainLightning>().target = player.transform.FindChild("spon").gameObject;
        chainLightningClon.GetComponent<ChainLightning>().player = player;
    }

    IEnumerator Lightning()
    {
        Reset();
        Finding();

        if ((saveCount < attackCount) && (enemies.Count != 0))
        {
            saveTime += Time.deltaTime;

            if (saveTime >= attackDelay)
            {
                //원래요기
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (enemies[i] != null)
                    {
                        if (enemies[i].GetComponent<M_HP>() != null)
                        {
                            enemies[i].GetComponent<M_HP>()._TakeDamage((int)damge);
                        }
                    }
                }
                DrowTail();
                DrowLine();
                saveCount += 1;
                saveTime = 0;
            }
            Debug.Log("작동중입니다. 카운팅 : " + saveCount);
        }
        else
        {
            Destroy(gameObject);
        }
        yield return null;
    }

    public void Finding()
    {
        for (int count = 0; count < enemyLimite; count++)
        {
            //라이트닝의 마지막지점으로부터 가장 가까운 적찾기
            for (int i = 0; i < enemy.Count; i++)
            {
                if(enemy[i] != null)
                {
                    if (enemies.Count == 0)
                    {
                        CheckDist(enemy[i], target);
                    }
                    else
                    {
                        CheckDist(enemy[i], enemies[enemies.Count - 1]);
                    }
                }
            }

            Compare();
        }
    }

    public void Reset()
    {
        enemy.Clear();
        enemies.Clear();
        enemy.AddRange(FindObjectsOfType<EnemyType>()); //적대상 검색
    }

    //거리차 구하기
    public void CheckDist(EnemyType e1, GameObject player)
    {
        e1.dist = Mathf.Abs((e1.myTransform.position - player.transform.position).magnitude);
    }

    public void CheckDist(EnemyType e1, EnemyType e2)
    {
        e1.dist = Mathf.Abs((e1.myTransform.position - e2.myTransform.position).magnitude);
    }

    //가장 가까운적 찾기
    public void Compare()
    {
        EnemyType e = new EnemyType();
        
        e.dist = fDist + 1; //최초 가진거리가 최대비교거리의 밖으도록 설정
        int eNumber = -1; //e의 배열번호를 기억하는 부분. 0이상일경우 문제가 발생할수 있기에 -1로설정
        for (int i = 0; i < enemy.Count; i++)
        {
            if (enemy[i] != null)
            {
                if (e.dist > enemy[i].dist)
                {
                    if (enemy[i].dist != 0)
                    {
                        if (enemies.Count == 0)
                        {
                            if (enemy[i].dist < fDist)
                            {
                                e = enemy[i];
                                eNumber = i;
                            }
                        }
                        else
                        {
                            if (enemy[i].dist < dist)
                            {
                                e = enemy[i];
                                eNumber = i;
                            }
                        }
                    }
                }
            }
        }
        if(e.dist<=fDist && eNumber >= 0)
        {
            enemies.Add(e);
            enemy[eNumber] = null;
        }
    }

    //테일을 그려준다.
    public void DrowTail()
    {
        foreach(EnemyType e in enemies)
        {
           GameObject prefab = Instantiate(tailPrefab, e.myTransform.position, Quaternion.identity) as GameObject;
            prefab.transform.parent = gameObject.transform;
            prefab.GetComponent<Destroy>().target = e.gameObject;
            prefab.GetComponent<Destroy>().destroyTime = attackDelay;
        }
    }

    //라인을 그려준다.
    public void DrowLine()
    {
        for(int i = 0; i<enemies.Count; i++)
        {
            GameObject line = Instantiate(linePrefab, enemies[i].myTransform.position, Quaternion.identity) as GameObject;
            line.transform.parent = gameObject.transform;
            line.GetComponent<Destroy>().target = enemies[i].gameObject;
            line.GetComponent<Destroy>().destroyTime = attackDelay;
            if (i==0)
            {
                line.GetComponent<LightningBolt.ChainLightningBolt>().StartObject = target;
            }
            else
            {
                line.GetComponent<LightningBolt.ChainLightningBolt>().StartObject = enemies[i - 1].myTransform.gameObject;
            }
            line.GetComponent<LightningBolt.ChainLightningBolt>().EndObject = enemies[i].myTransform.gameObject;
        }
    }
}
