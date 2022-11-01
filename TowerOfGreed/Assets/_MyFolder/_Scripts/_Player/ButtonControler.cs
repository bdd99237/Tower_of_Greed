using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonControler : MonoBehaviour {

    [SerializeField]
    GameObject newStartWindow;

    [SerializeField]
    GameObject settingPlayer;

    [SerializeField]
    GameObject loadWindow;
    [SerializeField]
    Text[] slotClass;
    [SerializeField]
    Text[] slotStage;

    [SerializeField]
    int slotNum;

    int deleteNum;

    [SerializeField]
    GameObject loadDeleteWindow;

    
    //[SerializeField]
   // GameObject scoreWindow;

    [SerializeField]
    GameObject exitWindow;

    [SerializeField]
    GameObject pauseWindow;

    [SerializeField]
    GameObject saveWindow;
    
    //초기화 부분
    void Start()
    {
        Time.timeScale = 1.0f;

        if(newStartWindow != null)
        newStartWindow.active = false;

        if (settingPlayer != null)
            settingPlayer.active = false;

        if (loadWindow != null)
            loadWindow.active = false;

        if (loadDeleteWindow != null)
            loadDeleteWindow.active = false;

       // if (scoreWindow != null)
        //    scoreWindow.active = false;

        if (exitWindow != null)
            exitWindow.active = false;

        if (pauseWindow != null)
            pauseWindow.active = false;

        if (saveWindow != null)
            saveWindow.active = false;

    }

    ////////////메인씬에서 사용//////////////////
    //새로시작관련 버튼 스크립트
	public void NewStart() //새로하기 버튼
    {
        GameManager.Instance.CheckSave();

        if(GameManager.Instance.fullFile)
        {
            newStartWindow.active = true;
        }
        else
        {
            GameManager.Instance.playerClass = null;
            settingPlayer.active = true;
        }
    }

    public void NewStartYes() //새로하기 창 yes
    {
        GameManager.Instance.playerClass = null;
        newStartWindow.active = false;
        settingPlayer.active = true;
    }

    public void NewStartNo() //새로하기 창 No
    {
        newStartWindow.active = false;
    }

    public void SelectWarrior()
    {
        GameManager.Instance.playerClass = "Warrior";
    }

    public void SelectMagician()
    {
        GameManager.Instance.playerClass = "Magician";
    }

    public void StartGame()
    {
        if(GameManager.Instance.playerClass != null)
        {
            GameManager.Instance.playerStage = 1;
            GameManager.Instance.ClassSetting();
            GameManager.Instance.artefact.Clear();
            Application.LoadLevel("stage1");
        }
    }

    //이어하기관련 버튼 스크립트
    public void LoadGame() //로드게임 버튼
    {
        loadWindow.active = true;
        Info_Reload();
    }

    public void LoadClose() //로드게임 셀렉트 클로즈버튼
    {
        loadWindow.active = false;
    }

    public void SettingClose() //캐릭터 생성 부분 클로즈버튼
    {
        settingPlayer.active = false;
    }

    public void LoadGameStart(int slotNum) //세이브 시작
    {
        GameManager.Instance.Load(slotNum);
        if (GameManager.Instance.isAlive)
        {
            GameManager.Instance.ClassSetting();
            GameManager.Instance.isArtefactLoad = true;
            GameManager.Instance.loadSlotNum = slotNum;
            Application.LoadLevel("stage1");
        }
    }

    public void LoadGameDelete(int slotNum) //세이브삭제버튼
    {
        deleteNum = slotNum;
        loadDeleteWindow.active = true;
    }

    public void LoadGameDeleteYes() //세이브파일 삭제확인
    {
        GameManager.Instance.Delete(deleteNum);
        loadDeleteWindow.active = false;
        Info_Reload();
    }

    public void LoadGameDeleteNo() //세이브파일 삭제취소
    {
        deleteNum = 0; //삭제할 세이브파일넘버를 저장해놓은 변수를 초기화
        loadDeleteWindow.active = false;
    }

    /*
    //기록관련 버튼 스크립트
    public void Record() //점수버튼
    {
        scoreWindow.active = true;
    }

    public void RecordClose()
    {
        scoreWindow.active = false;
    }
    */

    //게임종료관련 버튼 스크립트
    public void GameExit() //게임종료버튼
    {
        exitWindow.active = true;
    }

    public void GameExitYes() //게임종료버튼
    {
        Application.Quit();
    }

    public void GameExitNo() //게임종료버튼
    {
        exitWindow.active = false;
    }

    //사운드조절관련 버튼 스크립트
    public void SoundVolume()
    {
        if (GameManager.Instance.bgm.isPlaying == true)
          {
          GameManager.Instance.bgm.Stop();
         }
         else
         {
        GameManager.Instance.bgm.Play();
        }
    }

    /////////////게임씬에서 사용///////////////////
    //일시정지
    public void Pause()
    {
        if(pauseWindow.active)
        {
            pauseWindow.active = false;
            Time.timeScale = 1.0f;
            
        }
        else
        {
            pauseWindow.active = true;
            Time.timeScale = 0f;
            
        }
    }

    //메인으로
    public void GoMain()
    {
        Application.LoadLevel("Main");
    }

    //세이브
    public void SaveButton()
    {
        saveWindow.active = true;
        Info_Reload();
    }

    public void SaveSlotButton(int slotNum)
    {
        GameManager.Instance.Save(slotNum);
        saveWindow.active = false;
    }

    void Info_Reload()
    {
        for(int i = 1; i<=slotNum;i++)
        {
            GameManager.Instance.LoadView(i);
            if(GameManager.Instance.isAlive)
            {
                if (GameManager.Instance.viewPlayerClass == "Warrior")
                {
                    slotClass[i-1].text = "직업 : 전사";
                }
                else if(GameManager.Instance.viewPlayerClass == "Magician")
                {
                    slotClass[i-1].text = "직업 : 마법사";
                }
                slotStage[i-1].text = GameManager.Instance.viewPlayerStage.ToString();
            }
            else
            {
                slotClass[i - 1].text = "정보가 존재하지 않습니다.";
                slotStage[i - 1].text = "Information does not exist.";
            }
        }
    }
}

//////////// onpointerclick   /////////