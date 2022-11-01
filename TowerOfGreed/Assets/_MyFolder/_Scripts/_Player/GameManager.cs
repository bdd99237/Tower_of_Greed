using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region 싱글톤
    private static GameManager _instance = null;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //_instance = new GameManager();
            }

            return _instance;
        }
    }

    private GameManager() { }
    #endregion

    public AudioSource bgm; //배경음저장

    public string playerClass; //플레어 클래스

    public int playerStage; //플레이어 현재 스테이지

    public string viewPlayerClass; //저장보이게 플레어 클래스

    public int viewPlayerStage; //저장보이게 플레이어 현재 스테이지

    //기본셋팅변수
    public float PStartingHP = 0; //체력
    public float defaultDamge = 0; //데미지
    public float skil_1_Damge = 0;
    public float skil_2_Damge = 0;
    public float del = 0; //기본공격의 딜레이
    public float speed = 0; //속도

    //최종스텟변수
    public float result_HP = 0; //체력
    public float result_DefaultDamge = 0; //데미지
    public float result_Skil_1_Damge = 0;
    public float result_Skil_2_Damge = 0;
    public float result_Del = 0; //공속퍼센트
    public float result_Speed = 0; //속도

    //아티펙트 계수
    public float modulus_HP = 1.0f; //체력
    public float modulus_defaultDamge = 1.3f; //데미지
    public float modulus_Skil_Damge = 1.5f;
    public float modulus_Del = 1.0f; //딜레이 계수
    public float modulus_Speed = 1.0f; //속도

    //아티펙트 수치
    public float increase_HP; //체력
    public float increase_defaultDamge; //데미지
    public float increase_Skil_Damge;
    public float increase_Del; //딜레이(퍼센트)
    public float increase_Speed; //속도

    public bool isAlive; //로드파일 존재여부

    public bool isArtefactLoad = false; //아티펙트 로드를 위한 부분.
    public int loadSlotNum = 0; //로드된 슬롯의 번호저장.

    public bool fullFile = false;

    public List<Artefact> artefact = new List<Artefact>();

    void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject); //이 스크립트를 가진 오브젝트가 파괴되지 않는다.
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (bgm == null && GameObject.FindGameObjectWithTag("BGM")) //BGM태그를 가진 오브젝트가 있다면
        {
            bgm = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
            bgm.Play();
        }
        
        if (artefact.Count != 0)
        {
            for (int i = 0; i < artefact.Count; ++i)
            {
                if (artefact[i].artefact_index == 0)
                {
                    artefact[i].Action();
                    break;
                }
            }
        }
        
        if (!isArtefactLoad)
        {
            ResultStatus();
        }
    }

    public void CheckSave()
    {
        fullFile = false;

        if (File.Exists(Application.persistentDataPath + "/playerInfo" + 1 + ".dat") && File.Exists(Application.persistentDataPath + "/playerInfo" + 2 + ".dat"))
        {
            fullFile = true;
        }
    }

    public void Save(int num)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo" + num + ".dat");
        PlayerData data = new PlayerData();
        data.playerClass = playerClass;
        data.stage = playerStage;
        if(data.artefactData.Count != 0)
        {
            data.artefactData.Clear(); //리스트 초기화
        }
        if(artefact.Count != 0)
        {
            for (int i = 0; i < artefact.Count; ++i)
            {
                Debug.Log(artefact[i].artefact_index);
                data.artefactData.Add(artefact[i].artefact_index);
                Debug.Log(artefact[i].count);
                data.artefactData.Add(artefact[i].count);
            }
        }
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load(int num)
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo" + num + ".dat"))
        {
            isAlive = true; //파일존재
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo" + num + ".dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            playerClass = data.playerClass;
            playerStage = data.stage;
        }
        else
        {
            isAlive = false; //파일 미존재
        }
    }

    public void Load_Artefact(int num)
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo" + num + ".dat"))
        {
            isAlive = true; //파일존재
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo" + num + ".dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            
            if (artefact.Count != 0)
            {
                for (int a = 0; a < artefact.Count; a++)
                {
                    if (artefact[a] != null)
                    {
                        Destroy(artefact[a].gameObject);
                    }
                }
                artefact.Clear();
            }
            if (data.artefactData.Count != 0)
            {
                for (int i = 0; i < data.artefactData.Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        GameObject artefact_prefbe = Instantiate(Resources.Load<GameObject>("Artefact"), Vector3.zero, Quaternion.identity) as GameObject;
                        artefact_prefbe.transform.parent = GameObject.Find("ArtefactGrid").transform;
                        artefact_prefbe.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        artefact_prefbe.transform.localScale = new Vector3(1, 1, 1);

                        PlayerManager.Instance.AddArtefectScript(artefact_prefbe, data.artefactData[i]);

                        ArtefactListAdd(artefact_prefbe);
                    }
                    else
                    {
                        artefact[artefact.Count - 1].count = data.artefactData[i];
                        Debug.Log(data.artefactData[i]);
                    }
                }
            }
        }
        else
        {
            isAlive = false; //파일 미존재
        }
    }


    public void LoadView(int num)
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo" + num + ".dat"))
        {
            isAlive = true; //파일존재
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo" + num + ".dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            viewPlayerClass = data.playerClass;
            viewPlayerStage = data.stage;
        }
        else
        {
            isAlive = false; //파일 미존재
        }
    }

    public void Delete(int num)
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo" + num + ".dat"))
        {
            File.Delete(Application.persistentDataPath + "/playerInfo" + num + ".dat");
        }
    }

    public void ClassSetting()
    {
        if (playerClass == "Warrior")
        {
            defaultDamge = 20.0f;
            del = 0.3f;
            PStartingHP = 1000.0f;
            speed = 4.0f;
            skil_1_Damge = 70;
            skil_2_Damge = 25;
        }
        else if (playerClass == "Magician")
        {
            defaultDamge = 15.0f;
            del = 0.3f;
            PStartingHP = 850.0f;
            speed = 4.0f;
            skil_1_Damge = 50.0f;
            skil_2_Damge = 85.0f;
        }
    }

    public void ArtefactListAdd(GameObject add)
    {
        artefact.Add(add.GetComponent<Artefact>());
    }

    public void ResultStatus()
    {
        if(artefact.Count != 0)
        {
            for (int i = 0; i < artefact.Count; ++i)
            {
                if (artefact[i].artefact_index == 0)
                {
                    result_HP = (PStartingHP + ((artefact[i].count * increase_HP) * modulus_HP));
                    result_DefaultDamge = (defaultDamge + ((artefact[i].count * increase_defaultDamge) * modulus_defaultDamge));
                    result_Skil_1_Damge = (skil_1_Damge + ((artefact[i].count * increase_Skil_Damge) * modulus_Skil_Damge));
                    result_Skil_2_Damge = (skil_2_Damge + ((artefact[i].count * increase_Skil_Damge) * modulus_Skil_Damge));
                    result_Del = (1 / (1 + ((artefact[i].count * increase_Del) * modulus_Del)));
                    result_Speed = (speed + ((artefact[i].count * increase_Speed) * modulus_Speed));
                    break; //스탯을 계산하였으므로 루프를 빠져나간다.
                }
            }
        }
        else
        {
            result_HP = PStartingHP;
            result_DefaultDamge = defaultDamge;
            result_Skil_1_Damge = skil_1_Damge;
            result_Skil_2_Damge = skil_2_Damge;
            result_Del = 1.0f;
            result_Speed = speed;
        }
    }
}

[Serializable]
class PlayerData
{
    public string playerClass;
    public int stage;
    public List<int> artefactData = new List<int>(); //테스트중!!
    //아이템
}
